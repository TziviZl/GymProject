using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class GlobalStudioClasses
    {
        public int Id { get; set; } 

        public string Name { get; set; } = null!; 

        public int Price { get; set; } 

        public int MaxParticipantsNumber { get; set; } 

        public string TrainerId { get; set; } = null!; 

        public virtual Trainer Trainer { get; set; } = null!; 
    }
}