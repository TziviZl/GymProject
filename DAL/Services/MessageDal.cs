using DAL.Api;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class MessageDal:IMessageDal
    {
        private readonly DB_Manager _dB_Manager;

        public MessageDal(DB_Manager dB_Manager)
        {
            _dB_Manager = dB_Manager;
            
        }

        public List<Message> GetAllMessages()
        {
            return  _dB_Manager.Messages.ToList();
        }

        public void AddMessage(Message message)
        {
            _dB_Manager.Messages.Add(message);
            _dB_Manager.SaveChanges();
        }
    }
}
