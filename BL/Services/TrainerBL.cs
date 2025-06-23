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

        public TrainerBL(ITrainerDal trainerDal,IMapper mapper)
        {
            _trainerDal = trainerDal;
            _mapper = mapper;
        }

        public int GetNumOfGymnasts(string trainerId, DateTime courseDate)
        {
            int classId = _trainerDal.GetClassId(trainerId, courseDate);
            return _trainerDal.GetGymnasts(classId).Count;
        }

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



        public void NewTrainer(M_Trainer m_trainer)
        {
  
            if (m_trainer == null)
            {
                throw new ArgumentNullException(nameof(m_trainer), "Trainer object cannot be null.");
            }

            if (!GetTrainerBySpecialization(m_trainer.Specialization))
            {
                Trainer trainer = _mapper.Map<Trainer>(m_trainer);
                _trainerDal.NewTrainer(trainer);
            }
            else
            {
                _trainerDal.NewBackupTrainer(_mapper.Map<BackupTrainer>(m_trainer));
            }
            _trainerDal.SaveChanges();

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

        public void UpdateTrainer(Trainer trainer)
        {
            if(trainer == null)
            {
                throw new ArgumentNullException(nameof(trainer), "Trainer object cannot be null.");


            }
            _trainerDal.UpdateTrainer(trainer);
           
        }

        public M_Trainer GetTrainerById(string trainerId)
        {
            Trainer trainer = _trainerDal.GetTrainerById(trainerId);
            if (trainer == null)
            {
                throw new ArgumentNullException(nameof(trainer), "Trainer object cannot be null.");
            }
            M_Trainer m_Trainer = _mapper.Map<M_Trainer>(trainer);
            return m_Trainer;
        }

      

        public bool GetTrainerBySpecialization(string spec)
        {
          Trainer trainer  = _trainerDal.GetTrainerBySpecialization(spec);
          return trainer != null;

        }

        public List<BackupTrainer> GetBackupTrainers()
        {
            return _trainerDal.GetBackupTrainers();
        }


        public (List<string> Emails, List<GlobalStudioClass> ClassesWithoutTrainer) DeleteAndReplaceTrainer(string trainerId)
        {
            var newTrainers = _trainerDal.BackupTrainers(trainerId);

            if (newTrainers == null || newTrainers.Count == 0)
            {
                // אין מחליף → מבטלים שיעורים ומעדכנים גימנסטים
                _trainerDal.CancelTrainerClassesAndUpdateGymnasts(trainerId);
                var emails = _trainerDal.GetGymnastEmails(trainerId);

                // עדכון שיעורים ל־TrainerId=null
                var classesWithoutTrainer = _trainerDal.GetClassesWithoutTrainerByTrainerId(trainerId);

                // עכשיו מוחקים את המאמן
                _trainerDal.DeleteTrainer(trainerId);

                return (emails, classesWithoutTrainer);
            }

            // יש מחליף - טיפול רגיל
            var backupTrainer = newTrainers.First();
            _trainerDal.PromoteBackupTrainerToTrainer(backupTrainer);
            _trainerDal.AssignTrainerToStudioClass(trainerId, backupTrainer.Id);
            _trainerDal.DeleteTrainer(trainerId);

            return (null, null);
        }




    }
}
