using BL.Api;
using BL.Models;
using DAL.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class GymnastBL:IGymnastBL
    {
        private readonly IGymnastDal _gymnastDal;

        public GymnastBL(IGymnastDal gymnastDal)
        {
            _gymnastDal = gymnastDal;
        }
    }
}
