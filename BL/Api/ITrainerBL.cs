using BL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface ITrainerBL
    {
        public int GetNumOfGymnasts(string trainerId, DateTime courseDate);
        public List<ModelTrainerBL> GetAllTrainers();

        public List<ModelStudioClasses> GetStudioClasses(string trainerId);

        public bool NewTrainer(Trainer trainer);

      }
}
