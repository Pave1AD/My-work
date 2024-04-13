using BLL.Entities;
using Core.Enums;

namespace BLL.Abstractions.Services
{
    public interface IImageService
    {
        Task<Image?> GetImageByIdAsync(int id);
        Task<Image> CreateImageAsync(Image image, User? user);
        Task<Image> CreateImageFromStreamAsync(Stream stream, ImageTypes imageType, User? user);
        Task<Image> CreatePlaceholderImageAsync(ImageTypes placeholderType, User user);
        Task DeleteImageAsync(Image image);
        Task UpdateImageAsync(Image image);
    }
}
