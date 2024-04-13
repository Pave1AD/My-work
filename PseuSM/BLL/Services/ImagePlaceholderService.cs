using BLL.Abstractions.Services;
using BLL.Entities;
using Core.Enums;

namespace BLL.Services
{
    public class ImagePlaceholderService : IImagePlaceholderService
    {
        public ImagePlaceholderService()
        {

        }

        public Task CreateImagePlaceholderFromStreamAsync(Stream stream, ImageTypes placeholderType)
        {
            throw new NotImplementedException();
        }

        public Task DeleteImagePlaceholderAsync(ImagePlaceholder placeholder)
        {
            throw new NotImplementedException();
        }

        public Task<ImagePlaceholder> GetImagePlaceholderByImageType(ImageTypes placeholderType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImagePlaceholder>> GetImagePlaceholdersByImageType(ImageTypes placeholderType)
        {
            throw new NotImplementedException();
        }
    }
}
