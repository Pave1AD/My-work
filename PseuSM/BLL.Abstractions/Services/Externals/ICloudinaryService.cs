namespace BLL.Abstractions.Services.Externals
{
    public interface ICloudinaryService
    {
        public Task<string> UploadImageAsync(Stream stream, string imageName);
        public Task<string> UploadVideoAsync(Stream stream, string videoName);
        public Task DeleteVideoAsync(string publicId);
        public Task DeleteImageAsync(string publicId);
    }
}
