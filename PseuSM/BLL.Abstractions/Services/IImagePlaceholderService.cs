using BLL.Entities;
using Core.Enums;

namespace BLL.Abstractions.Services
{
    public interface IImagePlaceholderService
    {
        public Task CreateImagePlaceholderFromStreamAsync(Stream stream, ImageTypes placeholderType);
        public Task<IEnumerable<ImagePlaceholder>> GetImagePlaceholdersByImageType(ImageTypes placeholderType);
        public Task<ImagePlaceholder> GetImagePlaceholderByImageType(ImageTypes placeholderType);
        public Task DeleteImagePlaceholderAsync(ImagePlaceholder placeholder);
    }
}
