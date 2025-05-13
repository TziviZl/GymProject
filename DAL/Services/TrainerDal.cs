using DAL.Api;
using DAL.Models;
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
        public int GetClassId(string trainerId, DateTime courseDate)
        {
            var classId = _dbManager.StudioClasses.FirstOrDefault(t => t.TrainerId == trainerId && t.Date == courseDate);
            if (classId != null)
            {
                return classId.Id;
            }
            else return -1;
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
                var trainer = _dbManager.Trainers
                    .FirstOrDefault(t => t.Id == trainerId);

                if (trainer == null)
                {
                    throw new Exception("This trainer is not exists");
                }

                return trainer.StudioClasses.ToList();
            }


   
        public bool NewTrainer(Trainer trainer)
        {
            try
            {
                _dbManager.Trainers.Add(trainer);

                _dbManager.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
