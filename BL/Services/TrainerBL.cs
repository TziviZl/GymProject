using BL.Api;
using BL.Models;
using DAL.Api;
using DAL.Models;
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
        public List<ModelTrainerBL> GetAllTrainers()
        {
            var previous = _trainerDal.GetAllTrainers();
            List<ModelTrainerBL> updated = new();
            previous.ForEach(t => updated.Add
                (new ModelTrainerBL()
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Level = t.Level,
                    NumOfStudioClasses = t.StudioClasses.Count
                }));
            return updated;
            // נלך לדל
            // נביא נתוני מאמנים
            // נערוך אותם למבנה הרצוי
            //ונחזיר

        }

        public bool NewTrainer(Trainer trainer)
        {
           return _trainerDal.NewTrainer(trainer);

        }

        public List<M_StudioClasses> GetStudioClasses(string trainerId)
        {
            if (_trainerDal.GetStudioClasses(trainerId).IsNullOrEmpty())
            {
                return new List<M_StudioClasses>();
            }
            List<StudioClass> studioClasses = _trainerDal.GetStudioClasses(trainerId);
            List<M_StudioClasses> m_StudioClasses = new();
            studioClasses.ForEach(t => m_StudioClasses.Add
            (new M_StudioClasses()
            {
                Name = t.Name,
                Level = t.Level,
                Date = t.Date
            }));

            return m_StudioClasses;
        }
    }
}
