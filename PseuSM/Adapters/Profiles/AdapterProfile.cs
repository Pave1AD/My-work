using Adapters.Entities;
using AutoMapper;
using BLLUser = BLL.Entities.User;
using BLLImage = BLL.Entities.Image;

namespace Adapters.Profiles
{
    public class AdapterProfile : Profile
    {
        public AdapterProfile()
        {
            CreateMap<BLLUser, RegisterUser>().ReverseMap();
            CreateMap<BLLUser, LoginUser>().ReverseMap();
            CreateMap<BLLUser, User>().ReverseMap();
            CreateMap<BLLImage, Image>().ReverseMap();
        }
    }
}
