using BLL.Abstractions.Services;
using BLL.Abstractions.Services.Externals;
using BLL.Entities;
using BLL.Services.BaseServices;
using BLL.Services.Parameters;
using Core;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using DALCloudinaryImage = DAL.Entities.Cloudinary.CloudinaryImage;
using DALImage = DAL.Entities.Image;

namespace BLL.Services
{
    public class CloudinaryImageService : ServiceBase<DALCloudinaryImage>, IImageMetadataService<CloudinaryImageParameters>
    {
        private readonly CloudinaryParameters _cloudinaryParameters;
        private readonly IDALImageService _imageService;
        private readonly ICloudinaryService _cloudinaryService;

        public CloudinaryImageService(
            IUnitOfWork unitOfWork,
            IDALImageService imageService,
            ICloudinaryService cloudinaryService, 
            IOptions<CloudinaryParameters> cloudinaryParametersOptions) : base(unitOfWork)
        {
            _cloudinaryParameters = cloudinaryParametersOptions.Value ?? throw new ArgumentNullException(nameof(cloudinaryParametersOptions));
            _imageService = imageService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task AttachImageToAllMetadataFromSourceImageAsync(Image sourceImage, Image image)
        {
            DALImage dalImage = await GetDALImageAsync(image.Id);

            IRepository<DALCloudinaryImage, int, Expression<Func<DALCloudinaryImage, bool>>> cloudinaryImageRepository = await GetRepositoryAsync();

            IEnumerable<DALCloudinaryImage> cloudinaryImagesAttachedToSource = await cloudinaryImageRepository.GetEntitiesByPredicateAsync(ci => ci.Images.Any(img => img.Id == sourceImage.Id));

            foreach (DALCloudinaryImage cloudinaryImage in cloudinaryImagesAttachedToSource)
            {
                cloudinaryImage.Images.Add(dalImage);
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task CreateMetadataFromStreamAsync(Stream stream, Image image, CloudinaryImageParameters parameters)
        {
            DALImage dalImage = await GetDALImageAsync(image.Id);

            DateTime now = DateTime.Now;

            string publicId = await _cloudinaryService.UploadImageAsync(stream, $"{(parameters.User == null ? "placeholder" : $"{parameters.User.Id}_{parameters.User.Login}")}_{now.Year}_{now.Day}_{now.Minute}");

            var cloudinaryImage = new DALCloudinaryImage
            {
                PublicId = publicId,
            };

            cloudinaryImage.Images.Add(dalImage);

            IRepository<DALCloudinaryImage, int, Expression<Func<DALCloudinaryImage, bool>>> cloudinaryImageRepository = await GetRepositoryAsync();

            await cloudinaryImageRepository.CreateEntityAsync(cloudinaryImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMetadataAsync(Image image)
        {
            DALImage dalImage = await GetDALImageAsync(image.Id);

            IRepository<DALCloudinaryImage, int, Expression<Func<DALCloudinaryImage, bool>>> cloudinaryImageRepository = await GetRepositoryAsync();

            IEnumerable<DALCloudinaryImage> delCloudinaryImages = await cloudinaryImageRepository.GetEntitiesByPredicateAsync(cloudinaryImage => cloudinaryImage.Images.Any(img => img.Id == image.Id));

            foreach (DALCloudinaryImage cloudinaryImage in delCloudinaryImages)
            {
                if (cloudinaryImage.Images.Count > 1)
                {
                    cloudinaryImage.Images.Remove(dalImage);
                }
                else
                {
                    await _cloudinaryService.DeleteImageAsync(cloudinaryImage.PublicId);
                    await cloudinaryImageRepository.DeleteEntityAsync(cloudinaryImage.Id);
                }
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<string> GetReferenceAsync(Image image)
        {
            IRepository<DALCloudinaryImage, int, Expression<Func<DALCloudinaryImage, bool>>> cloudinaryImageRepository = await GetRepositoryAsync();

            IEnumerable<DALCloudinaryImage> cloudinaryImagesAttachedToImage = await cloudinaryImageRepository.GetEntitiesByPredicateAsync(cloudinaryImage => cloudinaryImage.Images.Any(img => img.Id == image.Id));

            if (!cloudinaryImagesAttachedToImage.Any())
            {
                throw new ArgumentException("There is no Cloudinary metadata attached to image");
            }

            DALCloudinaryImage cloudinaryImageAttachedToImage = cloudinaryImagesAttachedToImage.First();

            return new Uri(new Uri(_cloudinaryParameters.DefaultImageUrl), cloudinaryImageAttachedToImage.PublicId).AbsoluteUri;
        }

        private async Task<DALImage> GetDALImageAsync(int id)
        {
            DALImage? dalImage = await _imageService.GetDALImageByIdAsync(id) ?? throw new ArgumentException($"There is no image with id {id}");

            return dalImage;
        }
    }
}
