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
    }
}
