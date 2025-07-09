using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IMessageDal
    {
        public void AddMessage(Message message);
        public List<Message> GetAllMessages();


    }
}
