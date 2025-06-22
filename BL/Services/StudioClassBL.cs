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
    public class StudioClassBL:IStudioClassBL
    {
        private readonly IStudioClassDal _StudioClass;
        private readonly IGymnastDal _GymnastDal;
        private readonly ITrainerDal _trainerDal;

        private readonly IMapper _mapper;
        public StudioClassBL(IStudioClassDal StudioClass, IMapper mapper, IGymnastDal gymnastDal, ITrainerDal trainerDal)
        {
            _StudioClass = StudioClass;
            _mapper = mapper;
            _GymnastDal = gymnastDal;
            this._trainerDal = trainerDal;
        }
        public List<M_ViewStudioClasses> GetAllLessons()
        {
            var studioClasses = _StudioClass.GetAllLessons();

            if (studioClasses == null || !studioClasses.Any())
            {
                return new List<M_ViewStudioClasses>();
            }

            return _mapper.Map<List<M_ViewStudioClasses>>(studioClasses);
        }

        public StudioClass GetById(int studioClassId)
        {
            var studioClass= _StudioClass.GetById(studioClassId);
            return studioClass;
        }

        public bool IsFull(int studioClassId)
        {
            return _StudioClass.GetById(studioClassId).CurrentNum <= 0;
        }

        public bool CancelClassAndNotifyGymnasts(int classId)
        {
            _StudioClass.CancelStudioClass(classId);

            var gymnastIds = _trainerDal.GetGymnasts(classId);
            foreach (var gymnastId in gymnastIds)
            {
                var gymnast = _GymnastDal.GetGymnastById(gymnastId);

                gymnast.WeeklyCounter++;
                _GymnastDal.UpdateGymnast(gymnast);

                Console.WriteLine($"[INFO] Lesson Cancelled for {gymnast.FirstName} ({gymnast.Email})");
                Console.WriteLine($"Dear {gymnast.FirstName}, the lesson with ID {classId} has been cancelled. You may register for another lesson.");
            }

            return true;
        }

        public bool IsCancelled(int classId)
        {
            return _StudioClass.IsCancelled(classId);
        }




    }



}
