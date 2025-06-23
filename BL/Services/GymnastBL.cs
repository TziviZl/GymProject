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
        private readonly IStudioClassDal _studioClassDal;


        public GymnastBL(IGymnastDal gymnastDal,  IMapper mapper, IStudioClassDal studioClassDal)
        {
            _gymnastDal = gymnastDal;
            _mapper = mapper;
            _studioClassDal = studioClassDal;
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

        //public void NewGymnast(M_Gymnast m_gymnast)
        //{
        //    var gymnast = _mapper.Map<Gymnast>(m_gymnast);
        //    if (gymnast == null)
        //    {
        //        throw new Exception($"Gymnast with ID {gymnast.Id} not found.");
        //    }
        //    gymnast.Level = "A";
        //    gymnast.EntryDate = DateOnly.FromDateTime(DateTime.Now);
        //    _gymnastDal.AddGymnast(gymnast);
        //    _gymnastDal.SaveChanges();


        //}
        public void NewGymnast(M_Gymnast m_gymnast)
        {
            if (m_gymnast == null)
                throw new ArgumentNullException(nameof(m_gymnast));

            var gymnast = _mapper.Map<Gymnast>(m_gymnast);

            if (gymnast == null)
                throw new Exception("Mapping failed! Check your AutoMapper configuration.");

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
            var gimnast = _gymnastDal.GetGymnastById(gymnastId);
            gimnast.WeeklyCounter++;
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

        public void AddGymnastLesson(string gymnastId, int studioClassId)
        {
            var gymnast = GetGymnastById(gymnastId);
            var studioClass = _studioClassDal.GetById(studioClassId);

            if (studioClass == null)
                throw new ArgumentNullException(nameof(studioClass), "Studio class not found");

            var existingLessons = GetGymnastLessons(gymnastId);
            if (existingLessons.Any(l => l.Id == studioClassId))
                throw new Exception("You are already registered for this class.");

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
            }

            if (studioClass.CurrentNum > 0) {              

            studioClass.CurrentNum--;
            _gymnastDal.AddGymnastLesson(gymnastId, studioClass.Id);
            _gymnastDal.SaveChanges();
        }
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
        public List<M_ViewStudioClasses> GetGymnastLessons(string gymnastId)
        {
            var lessons = _gymnastDal.GetGymnastClassesByStudentId(gymnastId);

            var viewLessons = lessons
                .OrderByDescending(lesson => lesson.Class.Date)
                .Select(lesson => _mapper.Map<M_ViewStudioClasses>(lesson.Class)) 
                .ToList();

            return viewLessons;
        }

        public List<M_ViewContactGymnast> GetAllGymnastInSpecificClass(int studioClassId)
        {
            if (studioClassId == null)
                throw new ArgumentNullException( "Studio class not found");

            List<string> listID = _gymnastDal.GetAllGymnastInSpecificClass(studioClassId);
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
            if (level.Equals(' ')) 
                throw new Exception("No level entered.");
            List<Gymnast> gymnasts = _gymnastDal.GetAllGymnast();
            gymnasts = gymnasts.Where(g => g.Level == level.ToString()).ToList();
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
                .Where(g => g.BirthDate.HasValue)
                .Where(g =>
                {
                    int age = today.Year - g.BirthDate.Value.Year;
                    if (g.BirthDate.Value > today.AddYears(-age)) age--;
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
                throw new ArgumentException("No membership type entered.");
            List<Gymnast> allGymnasts = _gymnastDal.GetAllGymnast()
                .Where(g => g.MemberShipType != null)
                .ToList();

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
            var allGymnasts = _gymnastDal.GetAllGymnast();
            var filtered = allGymnasts
                .Where(g => g.EntryDate > joinDate)
                .ToList();
            var viewList = filtered
                .Select(g => _mapper.Map<M_ViewGymnast>(g))
                .ToList();
            return viewList;
        }
    }
}
