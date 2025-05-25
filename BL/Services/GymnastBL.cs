using AutoMapper;
using BL.Api;
using BL.Models;
using DAL.Api;
using DAL.Models;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class GymnastBL : IGymnastBL
    {
        private readonly IGymnastDal _gymnastDal;
        private readonly IMapper _mapper;


        public GymnastBL(IGymnastDal gymnastDal, IMapper mapper)
        {
            _gymnastDal = gymnastDal;
            _mapper = mapper;
        }

        public void AddMembershipType(string id, MembershipTypeEnum membershipType)
        {

            Gymnast gymnast = _gymnastDal.GetGymnastById(id);
            if (gymnast != null)
            {
                _gymnastDal.AddMembershipType(gymnast, membershipType);
                _gymnastDal.SaveChanges();
            }

            else
            {
                throw new Exception($"Gymnast with ID {id} not found.");
            }

        }

        public void NewGymnast(M_Gymnast m_gymnast)
        {
            var gymnast = _mapper.Map<Gymnast>(m_gymnast);
            if (gymnast == null)
            {
                throw new Exception($"Gymnast with ID {gymnast.Id} not found.");
            }
            gymnast.Level = "A";
            gymnast.EntryDate = DateOnly.FromDateTime(DateTime.Now);
            _gymnastDal.AddGymnast(gymnast);
            _gymnastDal.SaveChanges();


        }

        public void RemoveGymnastFromClass(string gymnastId, int classId)
        {
            var gymnastClass = _gymnastDal.GetGymnastClass(gymnastId, classId);
            if (gymnastClass == null)
            {
                throw new Exception($"Gymnast with ID {gymnastId} not found.");
            }

            _gymnastDal.RemoveGymnastFromClass(gymnastClass);

            var studioClass = _gymnastDal.GetStudioClass(classId);
            if (studioClass != null && studioClass.CurrentNum > 0)
            {
                studioClass.CurrentNum--;
            }

            _gymnastDal.SaveChanges();

        }

        public List<M_ViewGymnastBL> GetAllGymnast()
        {
            var dalGymnasts = _gymnastDal.GetAllGymnast();
            var blGymnasts = _mapper.Map<List<M_ViewGymnastBL>>(dalGymnasts);
            return blGymnasts;
        }
        public Gymnast GetGymnastById(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length != 9 || !id.All(char.IsDigit))
                throw new Exception("Invalid ID card");
            Gymnast gymnast = _gymnastDal.GetGymnastById(id);
            if (gymnast == null)
                throw new Exception("The ID card does not exist in the system.");
            return gymnast;

        }
        public void UpdateGymnanst(M_Gymnast m_Gymnast)
        {
            var gymnast = _mapper.Map<Gymnast>(m_Gymnast);
            if (gymnast == null)
                throw new Exception("An error occurred.");
            _gymnastDal.UpdateGymnast(gymnast);
            _gymnastDal.SaveChanges();


        }
        public void DeleteGymnast(string id)
        {
            Gymnast gymnast = GetGymnastById(id);
            var lessons = _gymnastDal.GetGymnastClassesByStudentId(id);

            foreach (var lesson in lessons)
            {
                _gymnastDal.GetStudioClass(lesson.ClassId).CurrentNum--;
                _gymnastDal.RemoveGymnastClass(lesson);
            }
            _gymnastDal.DeleteGymnast(id);
            _gymnastDal.SaveChanges();
        }
        public void AddGymnastLesson(string gymnastId, StudioClass studioClass)
        {
            var gymnast = GetGymnastById(gymnastId);

            if (studioClass == null)
                throw new ArgumentNullException(nameof(studioClass), "Studio class not found");
            switch (gymnast.MemberShipType)
            {
                case nameof(MembershipTypeEnum.Monthly_Standard):
                case nameof(MembershipTypeEnum.Yearly_Standard):
                    if (gymnast.WeeklyCounter <= 0)
                        throw new Exception("You have used up the maximum number of lessons for this week.");
                    gymnast.WeeklyCounter--;
                    break;
                case nameof(MembershipTypeEnum.Monthly_Pro):
                case nameof(MembershipTypeEnum.Yearly_Pro):
                    break;

                default:
                    throw new Exception("Unknown subscription type");
                    break;
            }
            if (studioClass.CurrentNum <= 0)
                throw new Exception("The class is fully booked!");

            studioClass.CurrentNum--;
            _gymnastDal.AddGymnastLesson(gymnastId, studioClass.Id);
            _gymnastDal.SaveChanges();


        }

    }
}
