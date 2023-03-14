using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Application.Common.Persistence.Photos;

namespace Infrastructure.ObjectStorage
{
    internal class PhotoStorage : IPhotoStorage
    {
        private const string BucketName = "public";
        private readonly string _publicUrl;
        private readonly IAmazonS3 _amazonS3Client;

        public PhotoStorage(ObjectStorageSettings settings)
        {
            _publicUrl = settings.PublicUrl;

            _amazonS3Client = new AmazonS3Client(settings.AccessKey, settings.SecretKey, new AmazonS3Config
            {
                ServiceURL = settings.ServiceUrl,
                ForcePathStyle = settings.ForcePathStyle
            });
        }

        public async Task<AddPhotoResponse> Add(AddPhotoRequest request, CancellationToken cancellationToken = default)
        {
            await _amazonS3Client.PutObjectAsync(new PutObjectRequest
            {
                BucketName = BucketName,
                Key = request.PhotoName,
                InputStream = request.Content,
                ContentType = request.ContentType
            }, cancellationToken);

            var resourceUrl = $"{_publicUrl}/{BucketName}/{request.PhotoName}";

            return new AddPhotoResponse(resourceUrl);
        }

        public Task Delete(DeletePhotoRequest request, CancellationToken cancellationToken = default)
        {
            return _amazonS3Client.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = BucketName,
                Key = request.PhotoName
            }, cancellationToken);
        }
    }
}
