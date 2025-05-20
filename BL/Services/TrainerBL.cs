using AutoMapper;
using BL.Api;
using BL.Models;
using DAL.Api;
using DAL.Models;
using DAL.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class TrainerBL : ITrainerBL
    {
        private readonly ITrainerDal _trainerDal;
        private readonly IMapper _mapper;

        public TrainerBL(ITrainerDal trainerDal)
        {
            _trainerDal = trainerDal;
        }

        public int GetNumOfGymnasts(string trainerId, DateTime courseDate)
        {
            int classId = _trainerDal.GetClassId(trainerId, courseDate);
            return _trainerDal.GetGymnasts(classId).Count;
        }

        // להחזיר רשימה של מאמנים כפי שרוצים לראות אותם במסך
        public List<M_ViewTrainerBL> GetAllTrainers()
        {
            var dalTrainer = _trainerDal.GetAllTrainers();
            List<M_ViewTrainerBL> blTrainer = _mapper.Map<List<M_ViewTrainerBL>>(dalTrainer);
            return blTrainer;
        }

 
            // נלך לדל
            // נביא נתוני מאמנים
            // נערוך אותם למבנה הרצוי
            //ונחזיר

        

        public bool NewTrainer( M_Trainer m_trainer)
        {

            Trainer trainer = _mapper.Map<Trainer>(m_trainer);
            return _trainerDal.NewTrainer(trainer);

        }
     
        public List<M_ViewStudioClasses> GetStudioClasses(string trainerId)
        {
            var studioClasses = _trainerDal.GetStudioClasses(trainerId);

            if (studioClasses == null || !studioClasses.Any())
            {
                return new List<M_ViewStudioClasses>();
            }

            return _mapper.Map<List<M_ViewStudioClasses>>(studioClasses);
        }
    }
}
