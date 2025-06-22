using BL.Api;
using DAL.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserTypeBL : IUserTypeBL
    {
        private readonly IUserTypeDal _dal;

        public UserTypeBL(IUserTypeDal dal)
        {
            _dal = dal;
        }

        public string? GetUserType(string id)
        {
            return _dal.GetUserTypeById(id);
        }
    }

}
