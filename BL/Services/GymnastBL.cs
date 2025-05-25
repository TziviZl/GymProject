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

        public bool NewGymnast(M_Gymnast m_gymnast)
        {
            var gymnast = _mapper.Map<Gymnast>(m_gymnast);
            gymnast.Level = "A";
            gymnast.EntryDate = DateOnly.FromDateTime(DateTime.Now);
            if (gymnast != null)
            {
                _gymnastDal.AddGymnast(gymnast);
                _gymnastDal.SaveChanges();
                return true;
            }
            return false;

        }

        public bool RemoveGymnastFromClass(string gymnastId, int classId)
        {
            var gymnastClass = _gymnastDal.GetGymnastClass(gymnastId, classId);
            if (gymnastClass == null)
            {
                return false;
            }

            _gymnastDal.RemoveGymnastFromClass(gymnastClass);

            var studioClass = _gymnastDal.GetStudioClass(classId);
            if (studioClass != null && studioClass.CurrentNum > 0)
            {
                studioClass.CurrentNum--;
            }

            _gymnastDal.SaveChanges();

            return true;
        }

        public List<M_ViewGymnastBL> GetAllGymnast()
        {
            var dalGymnasts = _gymnastDal.GetAllGymnast();
            var blGymnasts = _mapper.Map<List<M_ViewGymnastBL>>(dalGymnasts);
            return blGymnasts;
        }
        public Gymnast GetGymnastById(string id)
        {
            if (id == null)
                throw new Exception("No ID card entered.");
            Gymnast gymnast = _gymnastDal.GetGymnastById(id);
            if (gymnast == null)
                throw new Exception("The ID card does not exist in the system.");
            return gymnast;

        }
        public void UpdateGymnanst(M_Gymnast m_Gymnast)
        {
            var gymnast = _mapper.Map<Gymnast>(m_Gymnast);
            if (gymnast != null)
            {
                _gymnastDal.UpdateGymnast(gymnast);
                _gymnastDal.SaveChanges();
            }
            else throw new Exception("An error occurred.");

        }
        public void DeleteGymnast(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length != 9 || !id.All(char.IsDigit))
                throw new Exception("Invalid ID card");
            var student = _gymnastDal.GetGymnastById(id);
            if (student == null)
                throw new Exception("No gymnast found with the provided ID.");
            var lessons = _gymnastDal.GetGymnastClassesByStudentId(id);

            foreach (var lesson in lessons)
            {
                _gymnastDal.GetStudioClass(lesson.ClassId).CurrentNum--;
                _gymnastDal.RemoveGymnastClass(lesson);
            }
            _gymnastDal.DeleteGymnast(id);
            _gymnastDal.SaveChanges();
        }

    }
}
