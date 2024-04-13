using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using BLL.Services.Parameters;
using Core.Enums;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using DALImage = DAL.Entities.Image;
using DALImagePlaceholder = DAL.Entities.ImagePlaceholder;

namespace BLL.Services
{
    public class ImageService : ServiceBase<DALImage>, IImageService
    {
        private readonly IImageMetadataService<CloudinaryImageParameters> _imageMetadataService;
        private readonly IImagePlaceholderService _imagePlaceholderService;
        private readonly IMapper _mapper;

        public ImageService(
            IUnitOfWork unitOfWork,
            IImageMetadataService<CloudinaryImageParameters> imageMetadataService,
            IImagePlaceholderService imagePlaceholderService,
            IMapper mapper) : base(unitOfWork)
        {
            _imageMetadataService = imageMetadataService;
            _imagePlaceholderService = imagePlaceholderService;
            _mapper = mapper;
        }

        public async Task<Image?> GetImageByIdAsync(int id)
        {
            IRepository<DALImage, int, System.Linq.Expressions.Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            return _mapper.Map<Image?>(await imageRepository.GetEntityByIdAsync(id));
        }

        public async Task<Image> CreatePlaceholderImageAsync(ImageTypes placeholderType, User user)
        {
            ImagePlaceholder imagePlaceholder = await _imagePlaceholderService.GetImagePlaceholderByImageType(placeholderType);

            var copyOfPlaceholder = new Image()
            {
                Reference = imagePlaceholder.Image.Reference,
                Type = placeholderType,
            };

            await CreateImageAsync(copyOfPlaceholder, user);

            await _imageMetadataService.AttachImageToAllMetadataFromSourceImageAsync(copyOfPlaceholder, imagePlaceholder.Image);

            return copyOfPlaceholder;
        }

        public async Task<Image> CreateImageAsync(Image image, User? user)
        {
            IRepository<DALImage, int, System.Linq.Expressions.Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            DALImage newImage = _mapper.Map<DALImage>(image);

            if (user != null)
            {
                newImage.UserId = user.Id;
            }

            await imageRepository.CreateEntityAsync(newImage);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<Image>(newImage);
        }

        public async Task<Image> CreateImageFromStreamAsync(Stream stream, ImageTypes imageType, User? user)
        {
            IRepository<DALImage, int, System.Linq.Expressions.Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            var newImage = new DALImage()
            {
                Type = imageType,
            };

            if (user != null)
            {
                newImage.UserId = user.Id;
            }

            await imageRepository.CreateEntityAsync(newImage);

            await _unitOfWork.CommitAsync();

            Image mappedCopyOfDalImage = _mapper.Map<Image>(newImage);

            await _imageMetadataService.CreateMetadataFromStreamAsync(stream, mappedCopyOfDalImage, new CloudinaryImageParameters { User = user });

            mappedCopyOfDalImage.Reference = newImage.Reference = await _imageMetadataService.GetReferenceAsync(mappedCopyOfDalImage);

            await imageRepository.UpdateEntityAsync(newImage);

            await _unitOfWork.CommitAsync();

            return mappedCopyOfDalImage;
        }

        public async Task DeleteImageAsync(Image image)
        {
            IRepository<DALImage, int, System.Linq.Expressions.Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            await imageRepository.DeleteEntityAsync(image.Id);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateImageAsync(Image image)
        {
            IRepository<DALImage, int, System.Linq.Expressions.Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            DALImage? oldImage = await imageRepository.GetEntityByIdAsync(image.Id);

            DALImage newImage = _mapper.Map<DALImage>(image);
            newImage.UserId = oldImage?.UserId ?? newImage.UserId;

            await imageRepository.UpdateEntityAsync(newImage);

            await _unitOfWork.CommitAsync();
        }
    }
}
