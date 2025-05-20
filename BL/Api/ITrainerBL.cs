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
        public List<M_ViewTrainerBL> GetAllTrainers();

        public List<M_ViewStudioClasses> GetStudioClasses(string trainerId);

        public bool NewTrainer(M_Trainer m_Trainer);
       public bool UpdateTrainer(Trainer trainer);
        public M_Trainer GetTrainerById(string trainerId);

    }
}
