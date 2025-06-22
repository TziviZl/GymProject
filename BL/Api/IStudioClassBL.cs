using BL.Models;
using BL.Services;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IStudioClassBL
    {
        public List<M_ViewStudioClasses> GetAllLessons();
        public StudioClass GetById( int studioClassId);

        public bool IsFull(int studioClassId);

        bool CancelClassAndNotifyGymnasts(int classId);

        public bool IsCancelled(int classId);


    }
}
