using DAL.Api;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class TrainerDal : ITrainerDal
    {
        public readonly DB_Manager _dbManager;
        public TrainerDal(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }


        public List<Trainer> GetAllTrainers()
        {
           return  _dbManager.Trainers.ToList();
        }


        //public IEnumerable<GlobalStudioClasses> GetTrainerGlobalClasses(string trainerId)
        //{
        //    return _dbManager.GlobalStudioClasses.Where(g => g.TrainerId == trainerId).ToList();
        //}

        //public IEnumerable<StudioClass> GetStudioClassesByGlobalIds(List<int> globalClassIds)
        //{
        //    return _dbManager.StudioClasses.Where(s => globalClassIds.Contains(s.GlobalId)).ToList();
        //}

        public int GetClassId(string trainerId, DateTime courseDate)
        {
            var globalClass = _dbManager.GlobalStudioClasses
                .FirstOrDefault(t => t.TrainerId == trainerId);

            if (globalClass == null)
                return -1;

            var specificClass = _dbManager.StudioClasses
                .FirstOrDefault(t => t.GlobalId == globalClass.Id && t.Date == courseDate);

            if (specificClass == null)
                return -1;

            return specificClass.Id;
        }
        public List<string> GetGymnasts(int classId)
        {
            return _dbManager.GymnastClasses
                             .Where(c => c.ClassId == classId)
                             .Select(c => c.GymnastId)
                             .ToList();
        }
        public void PromoteBackupTrainerToTrainer(BackupTrainer backupTrainer)
        {
            // אם המאמן כבר קיים – לא מוסיפים שוב
            if (_dbManager.Trainers.Any(t => t.Id == backupTrainer.Id))
                return;

            // הוספת המאמן לטבלת Trainer
            var newTrainer = new Trainer
            {
                Id = backupTrainer.Id,
                FirstName = backupTrainer.FirstName,
                LastName = backupTrainer.LastName,
                BirthDate = backupTrainer.BirthDate,
                Specialization = backupTrainer.Specialization,
                Email = backupTrainer.Email,
            };

            _dbManager.Trainers.Add(newTrainer);

            // הסרה מטבלת BackupTrainers
            _dbManager.BackupTrainers.Remove(backupTrainer);

            _dbManager.SaveChanges();
        }



        public List<StudioClass> GetStudioClasses(string trainerId)
        {
            var globalIds = _dbManager.GlobalStudioClasses
                .Where(g => g.TrainerId == trainerId)
                .Select(g => g.Id)
                .ToList();

            return _dbManager.StudioClasses
                .Where(sc => globalIds.Contains(sc.GlobalId))
                .ToList();
        }



        public List<Gymnast> GetGymnastsByTrainerId(string trainerId)
        {
            var studioClasses = GetStudioClasses(trainerId);

            var gymnastIds = _dbManager.GymnastClasses
                .Where(gc => studioClasses.Select(sc => sc.Id).Contains(gc.ClassId))
                .Select(gc => gc.GymnastId)
                .Distinct()
                .ToList();

            return _dbManager.Gymnasts
                .Where(g => gymnastIds.Contains(g.Id))
                .ToList();
        }

        public List<string> GetGymnastEmails(string trainerId)
        {
            List<string> allEmails = new List<string>();
            List<Gymnast> gymnasts = GetGymnastsByTrainerId(trainerId);
            foreach (var gymnast in gymnasts)
            {
                allEmails.Add(gymnast.Email);
            }
            return allEmails.Distinct().ToList(); 
        }


        public void SaveChanges()
        {
            _dbManager.SaveChanges();
        }

        public void NewTrainer(Trainer trainer)
        {
                _dbManager.Trainers.Add(trainer);

        }

        public Trainer GetTrainerById(string trainerId)
        {
            return _dbManager.Trainers.FirstOrDefault(t => t.Id == trainerId);
        }


        public bool UpdateTrainer(Trainer trainer)
        {
            var existingTrainer = GetTrainerById(trainer.Id);
            if (existingTrainer == null)
                return false;

            existingTrainer.FirstName = trainer.FirstName;
            existingTrainer.LastName = trainer.LastName;
            existingTrainer.BirthDate = trainer.BirthDate;
            existingTrainer.Specialization = trainer.Specialization;
            existingTrainer.Email = trainer.Email;
            existingTrainer.Cell = trainer.Cell;


            return _dbManager.SaveChanges() > 0;
        }

        public List<BackupTrainer> BackupTrainers(string trainerId)
        {
            var trainer = GetTrainerById(trainerId);
            return _dbManager.BackupTrainers
                .Where(t => t.Specialization == trainer.Specialization)
                .ToList();
        }


        public List<GlobalStudioClass> GetClassesWithoutTrainerByTrainerId(string trainerId)
        {
            return _dbManager.GlobalStudioClasses
                .Where(c => c.TrainerId == null  )
                .ToList();
        }


        public bool DeleteTrainer(string trainerId)
        {
            // קודם מוחקים את כל הקשרים ל־GlobalStudioClasses
            var globalClasses = _dbManager.GlobalStudioClasses.Where(g => g.TrainerId == trainerId).ToList();

            foreach (var gc in globalClasses)
            {
                gc.TrainerId = null; // מבטל את הקשר
            }

            var trainer = _dbManager.Trainers.FirstOrDefault(t => t.Id == trainerId);
            if (trainer == null)
                return false;

            _dbManager.Trainers.Remove(trainer);
            _dbManager.SaveChanges();
            return true;
        }

        public void CancelTrainerClassesAndUpdateGymnasts(string trainerId)
        {
            var studioClasses = GetStudioClasses(trainerId);

            foreach (var studioClass in studioClasses)
            {
                studioClass.IsCancelled = true;

                var gymnastIds = _dbManager.GymnastClasses
                    .Where(gc => gc.ClassId == studioClass.Id)
                    .Select(gc => gc.GymnastId)
                    .ToList();

                var gymnasts = _dbManager.Gymnasts
                    .Where(g => gymnastIds.Contains(g.Id))
                    .ToList();

                foreach (var gymnast in gymnasts)
                {
                    gymnast.WeeklyCounter += 1;
                }
            }

            _dbManager.SaveChanges();
        }




        public void DeleteAllStudioClassesForTrainer(string trainerId)
        {
            var classes = _dbManager.GlobalStudioClasses.Where(c => c.TrainerId == trainerId).ToList();
            _dbManager.GlobalStudioClasses.RemoveRange(classes);
            // לא קוראים SaveChanges כאן
        }
        public bool AssignTrainerToStudioClass(string oldTrainerId, string newTrainerId)
        {
            var globalClasses = _dbManager.GlobalStudioClasses
                .Where(g => g.TrainerId == oldTrainerId)
                .ToList();

            foreach (var g in globalClasses)
            {
                g.TrainerId = newTrainerId;
            }

            _dbManager.SaveChanges();
            return true;
        }



        public Trainer GetTrainerBySpecialization(string spec)
        {
            Trainer trainer = _dbManager.Trainers.FirstOrDefault(t => t.Specialization.Equals(spec));

            return trainer;
        }


        public void NewBackupTrainer(BackupTrainer backupTrainer)
        {
            _dbManager.BackupTrainers.Add(backupTrainer);
        }

        public List<BackupTrainer> GetBackupTrainers()
        {
            return _dbManager.BackupTrainers.ToList();
        }
        public Gymnast GetGymnastById(string gymnastId)
        {
            return _dbManager.Gymnasts.FirstOrDefault(g => g.Id == gymnastId);
        }


    }
}
