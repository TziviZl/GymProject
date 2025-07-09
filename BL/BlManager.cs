using AutoMapper;
using BL.Api;
using BL.Services;
using DAL.Api;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    // מיצג את כל שכבת הביאל. 
    public class BlManager:IBL 
    {
        public ITrainerBL  Trainers  { get; set; }
        public IGymnastBL Gymnasts { get; set; }
        public IStudioClassBL StudioClass { get; set; }

        public IMessageBL Message { get; set; }

        public IMapper Mapper { get; set; }

        public BlManager(ITrainerBL trainerBL, IGymnastBL gymnastBL, IStudioClassBL StudioClassBL,IMessageBL messageBL, IMapper mapper)
        {
            Trainers = trainerBL;
            Gymnasts = gymnastBL;
            StudioClass = StudioClassBL;
            Message = messageBL;
            Mapper = mapper;
        }


    }
}
