using AutoMapper;
using BL.Models;
using BL.Services;
using DAL.Models;
using DAL.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Trainer, M_ViewTrainerBL>();
            CreateMap<M_ViewTrainerBL,Trainer >();
            CreateMap<StudioClass, M_ViewStudioClasses>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Global.Name))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Global.Trainer.FirstName));
            CreateMap<M_ViewStudioClasses,StudioClass >();
            CreateMap<Gymnast, GymnastBL>();
            CreateMap<GymnastBL, Gymnast>();
            CreateMap<Gymnast, M_ViewGymnastBL>();
            CreateMap<M_ViewGymnastBL, Gymnast>();
            CreateMap<Trainer, M_Trainer>();
            CreateMap<M_Trainer, Trainer>();
            CreateMap<M_Gymnast, Gymnast>();
            CreateMap<Gymnast, M_Gymnast>();
            CreateMap<Gymnast, M_ViewContactGymnast>();
            CreateMap<M_ViewContactGymnast, Gymnast>();
            CreateMap<Gymnast, M_ViewGymnast>();
            CreateMap<M_ViewGymnast,Gymnast> ();
            CreateMap<M_Trainer, BackupTrainer>();
            CreateMap<BackupTrainer, M_Trainer>();





        }
    }
}