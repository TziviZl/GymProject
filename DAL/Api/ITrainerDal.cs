using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface ITrainerDal
    {
        public int GetClassId(string  trainerId, DateTime courseDate);

        public List<string> GetGymnasts(int classId);

        public List<Trainer> GetAllTrainers();
        public List<GlobalStudioClass> GetStudioClasses(string trainerId);

        public void NewTrainer(Trainer trainer);

        public Trainer GetTrainerById(string trainerId);

        public bool UpdateTrainer(Trainer trainer);
        public bool DeleteTrainer(string trainerId);
        public List<BackupTrainer> BackupTrainers(string trainerId);
        public bool AssignTrainerToStudioClass(string oldTrainerId, string newTrainerId);
        public List<Gymnast> GetGymnastsByTrainerId(string trainerId);
        public List<string> GetGymnastEmails(string trainerId);

        public Trainer GetTrainerBySpecialization(string spec);

        public void NewBackupTrainer(BackupTrainer backupTrainer);

        public List<BackupTrainer> GetBackupTrainers();

        public void SaveChanges();


    }

}
