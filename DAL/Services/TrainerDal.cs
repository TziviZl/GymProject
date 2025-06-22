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
        private readonly DB_Manager _dbManager;
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
        public List<StudioClass> GetStudioClasses(string trainerId)
        {
            var studioClasses = _dbManager.StudioClasses
                .Include(sc => sc.Global)
                    .ThenInclude(g => g.Trainer)
                .Where(sc => sc.Global.TrainerId == trainerId)
                .ToList();

            return studioClasses;
        }



        public List<Gymnast> GetGymnastsByTrainerId(string trainerId)
        {
            List<StudioClass> studioClasses = GetStudioClasses(trainerId);
            List<string> allGymnastIds = new List<string>();

            foreach (var studioClass in studioClasses)
            {
                var gymnastsId = _dbManager.GymnastClasses
                    .Where(gc => gc.ClassId == studioClass.Id)
                    .Select(gc => gc.GymnastId)
                    .ToList();

                allGymnastIds.AddRange(gymnastsId);
            }

            allGymnastIds = allGymnastIds.Distinct().ToList();

            return _dbManager.Gymnasts
                .Where(g => allGymnastIds.Contains(g.Id))
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
            Trainer trainer = GetTrainerById(trainerId);

            var backupTrainers = _dbManager.BackupTrainers.Where(t => t.Specialization.Equals(trainer.Specialization)).ToList();
             return backupTrainers;

        }

        public bool DeleteTrainer(string trainerId)
        {
            Trainer trainer = _dbManager.Trainers.FirstOrDefault(t => t.Id == trainerId);
            if (trainer == null) 
                return false;
            _dbManager.Trainers.Remove(trainer);
            return true;
        }

        public bool AssignTrainerToStudioClass(string oldTrainerId, string newTrainerId)
        {
            var globalStudioClass = _dbManager.GlobalStudioClasses.FirstOrDefault(t=>t.Id.Equals(oldTrainerId));
            globalStudioClass.TrainerId = newTrainerId;
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


    }
}
