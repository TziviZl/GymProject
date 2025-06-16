using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IStudioClassDal
    {
        public List<StudioClass> GetAllLessons();

        public StudioClass GetById(int studioClassId);


    }
}
