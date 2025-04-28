using DAL.Api;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class GymnastDal:IGymnastDal
    {
        private readonly DB_Manager _dbManager;
        public GymnastDal(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }
    }
}
