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

        public IMapper Mapper { get; set; }
        // מתאמן
        // עובד
        // חדר 
        //.... 

        public BlManager()
        {
            DB_Manager db=new DB_Manager();
            ITrainerDal trainerDal = new DAL.Services.TrainerDal (db);
            // כאן צריך גם להזריק
            Trainers =new TrainerBL (trainerDal,Mapper);

            IGymnastDal gymnastDal = new DAL.Services.GymnastDal(db);
            Gymnasts = new GymnastBL(gymnastDal, Mapper);
        }
    }
}
