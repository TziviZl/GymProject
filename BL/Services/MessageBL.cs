using BL.Api;
using DAL.Api;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class MessageBL:IMessageBL
    {
        private readonly IMessageDal _messageDal;

        public MessageBL(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void AddMessage(Message message)
        {
           _messageDal.AddMessage(message);
        }

        public List<Message> GetAllMessages()
        {
          return  _messageDal.GetAllMessages();
        }
    }
}
