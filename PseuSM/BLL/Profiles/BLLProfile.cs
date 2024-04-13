using AutoMapper;
using BLL.Entities;
using DALUser = DAL.Entities.User;
using DALImage = DAL.Entities.Image;
using DALImagePlaceholder = DAL.Entities.ImagePlaceholder;
using DALCloudinaryImage = DAL.Entities.Cloudinary.CloudinaryImage;

namespace BLL.Profiles
{
    public class BLLProfile : Profile
    {
        public BLLProfile()
        {
            CreateMap<DALUser, User>().ReverseMap();
            CreateMap<DALImage, Image>().ReverseMap();
            CreateMap<DALImagePlaceholder, ImagePlaceholder>().ReverseMap();
            CreateMap<DALCloudinaryImage, CloudinaryImage>().ReverseMap();
        }
    }
}
