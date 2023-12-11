using AutoMapper;
using TraineeDemo.Models;
using Trainees.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Trainees.Profiles
{
    public class TraineeProfiles : Profile
    {
        public TraineeProfiles()
        {
            CreateMap<AddTraineeDto, Trainee>().ReverseMap();

            //CreateMap< Trainee, AddTraineeDto>();
        }
    }
}