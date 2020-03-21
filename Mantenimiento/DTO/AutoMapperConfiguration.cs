using AutoMapper;
using Mantenimiento.Models;

namespace Mantenimiento.DTO
{

 public class AutomapperProfile : Profile
    {
      

        public AutomapperProfile()
        {


            ConfigureDoctor();
           
        }

        
        private void ConfigureDoctor()
        {

            CreateMap<DoctorDto, Doctor>();

            CreateMap<Doctor, DoctorDto>().ForMember(dest => dest.Photo, opt => opt.Ignore());
                

        }
    }
}
