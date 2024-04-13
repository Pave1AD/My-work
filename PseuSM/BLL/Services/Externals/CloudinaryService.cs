using BLL.Abstractions.Services.Externals;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core;
using Microsoft.Extensions.Options;

namespace BLL.Services.Externals
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinaryAccountSettings> cloudinarySettingsOptions)
        {
            CloudinaryAccountSettings cloudinaryAccountSettings = cloudinarySettingsOptions.Value ?? throw new ArgumentNullException(nameof(cloudinarySettingsOptions));
            _cloudinary = new Cloudinary(new Account(cloudinaryAccountSettings.CloudName, cloudinaryAccountSettings.ApiKey, cloudinaryAccountSettings.ApiSecret));
        }

        public Task DeleteImageAsync(string publicId)
        {
            return DeleteAsync(publicId);
        }

        public Task DeleteVideoAsync(string publicId)
        {
            return DeleteAsync(publicId, ResourceType.Video);
        }

        public Task<string> UploadImageAsync(Stream stream, string imageName)
        {
            return UploadAsync<ImageUploadParams>(stream, imageName);
        }

        public Task<string> UploadVideoAsync(Stream stream, string videoName)
        {
            return UploadAsync<VideoUploadParams>(stream, videoName);
        }

        private async Task<string> UploadAsync<TParams>(Stream stream, string name)
            where TParams : RawUploadParams, new()
        {
            RawUploadResult uploadResult = await _cloudinary.UploadAsync(new TParams()
            {
                File = new FileDescription(name, stream)
            });

            if (uploadResult.Error is not null)
            {
                throw new InvalidOperationException($"Unable to upload image: {uploadResult.Error}");
            }

            return uploadResult.PublicId;
        }

        private async Task DeleteAsync(string publicId, ResourceType resourceType = ResourceType.Image)
        {
            DelResResult delResult = await _cloudinary.DeleteResourcesAsync(resourceType, publicId);

            if (delResult.Error is not null)
            {
                throw new InvalidOperationException($"Unable to delete image: {delResult.Error}");
            }
        }
    }
}
