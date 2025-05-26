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
        public void RemoveGymnastFromLesson(string gymnastId, StudioClass studioClass)
        {
            var gymnast = GetGymnastById(gymnastId);

            if (studioClass == null)
                throw new ArgumentNullException(nameof(studioClass), "Studio class not found");

            GymnastClass gymnastClass = _gymnastDal.GetGymnastClass(gymnastId, studioClass.Id);
            if (gymnastClass == null)
                throw new Exception("The gymnast was not registered for the class");
            studioClass.CurrentNum++;
            _gymnastDal.RemoveGymnastClass(gymnastClass);

            _gymnastDal.SaveChanges();



        }
        public List<M_ViewStudioClasses> GetGymnastLessons(string gymnastId, int numOfLesson)
        {

            var gymnast = GetGymnastById(gymnastId);

            var lessons = _gymnastDal.GetGymnastClassesByStudentId(gymnastId);
            var sortedLessons = lessons
         .OrderByDescending(lesson => _gymnastDal.GetStudioClass(lesson.ClassId).Date)
         .ToList();
            var lessonsToRemove = sortedLessons.Take(numOfLesson).ToList();
            var viewLessons = lessonsToRemove
            .Select(lesson => _mapper.Map<M_ViewStudioClasses>(lesson))
             .ToList();

            return viewLessons;
        }

        public List<M_ViewContactGymnast> GetAllGymnastInSpecificClass(StudioClass studioClass)
        {
            if (studioClass == null)
                throw new ArgumentNullException(nameof(studioClass), "Studio class not found");

            List<string> listID = _gymnastDal.GetAllGymnastInSpecificClass(studioClass.Id);
            var gymnasts = listID
                .Select(id => _gymnastDal.GetGymnastById(id))
                .Where(g => g != null)
                .ToList();
            var viewList = gymnasts
        .Select(g => _mapper.Map<M_ViewContactGymnast>(g))
        .ToList();

            return viewList;
        }
        public List<M_ViewGymnast> GetAllGymnastInSpecificLevel(char level)
        {
            if (level == null)
                throw new Exception("No level entered.");
            List<Gymnast> gymnasts = _gymnastDal.GetAllGymnast();
            gymnasts = gymnasts.Where(g => g.Level.Equals(level)).ToList();
            var viewList = gymnasts
.Select(g => _mapper.Map<M_ViewGymnast>(g))
.ToList();

            return viewList;

        }

        public List<M_ViewGymnast> GetAllGymnastByAge(int minAge, int maxAge)
        {
            if (minAge > maxAge)
                throw new ArgumentException("Minimum age cannot be greater than maximum age.");
            List<Gymnast> allGymnasts = _gymnastDal.GetAllGymnast();
            DateTime today = DateTime.Today;
            allGymnasts = allGymnasts
                .Where(g =>
                    {
                        int age = today.Year - g.BirthDate.Year;
                        if (g.BirthDate > today.AddYears(-age)) age--;
                        return age >= minAge && age <= maxAge;
                    })
                .ToList();
            var viewList = allGymnasts
                .Select(g => _mapper.Map<M_ViewGymnast>(g))
                .ToList();

            return viewList;

        }
        public List<M_ViewGymnast> GetAllGymnastByMembershipType(MembershipTypeEnum membershipType)
        {
            if (membershipType == null)
                throw new ArgumentException("No membership type entered."); List<Gymnast> allGymnasts = _gymnastDal.GetAllGymnast();

            allGymnasts = allGymnasts
                .Where(g => g.MemberShipType.Equals(membershipType.ToString()))
                .ToList();
            var viewList = allGymnasts
    .Select(g => _mapper.Map<M_ViewGymnast>(g))
    .ToList();

            return viewList;

        }

        public List<M_ViewGymnast> GetAllGymnastJoinedAfter(DateOnly joinDate)
        {
            List<Gymnast> allGymnasts = _gymnastDal.GetAllGymnast();

            allGymnasts = allGymnasts
                   .Where(g => g.EntryDate > joinDate)
                   .ToList();
            var viewList = allGymnasts
.Select(g => _mapper.Map<M_ViewGymnast>(g))
.ToList();

            return viewList;

        }
    }
}
