using BL.Api;
using DAL.Api;
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
           int classId =  _trainerDal.GetClassId(trainerId, courseDate);
           return _trainerDal.GetGymnasts(classId).Count;
        }
    }
}
