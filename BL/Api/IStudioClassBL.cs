using BL.Models;
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

    }
}
