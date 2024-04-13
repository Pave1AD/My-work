using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IImageMetadataService<TParams>
    {
        public Task CreateMetadataFromStreamAsync(Stream stream, Image image, TParams parameters);
        public Task AttachImageToAllMetadataFromSourceImageAsync(Image sourceImage, Image image);
        public Task<string> GetReferenceAsync(Image image);
        public Task DeleteMetadataAsync(Image metadata);
    }
}
