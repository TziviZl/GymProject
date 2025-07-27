using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exceptions
{
    public class TrainerOperationException : Exception
    {
        public TrainerOperationException(string message) : base(message) { }
    }
}
