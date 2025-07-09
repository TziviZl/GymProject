using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBL
    {
        public ITrainerBL Trainers { get; set; }
        public IGymnastBL Gymnasts { get; set; }
        public IStudioClassBL StudioClass { get; set; }

        public IMessageBL Message { get; set; }
    }
}