using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace TableTopFriend.Infrastructure.Services.Files;

public interface IFileStorageService
{
    Task<PutObjectResponse> UploadFileAsync(string path, string contentType, string fileName, Stream fileStream);
    Task<GetObjectResponse?> GetFileAsync(string path);
    Task<DeleteObjectResponse> DeleteFileAsync(string path);
}

public class AmazonS3FileStorageService : IFileStorageService
{
    private readonly IAmazonS3 _amazonS3;
    private readonly FileStorageServiceSettings _settings;
    private const string FILES_FOLDER = "files";
    private const string KEY_NOT_EXISTS_EXCEPTION_MESSAGE = "The specified key does not exist.";

    public AmazonS3FileStorageService(
        IAmazonS3 amazonS3,
        IOptions<FileStorageServiceSettings> options)
    {
        _amazonS3 = amazonS3;
        _settings = options.Value;
    }

    public async Task<PutObjectResponse> UploadFileAsync(
        string path,
        string contentType,
        string fileName,
        Stream fileStream)
    {
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _settings.Bucket,
            Key = $"{FILES_FOLDER}/{path}",
            ContentType = contentType,
            InputStream = fileStream,
            Metadata = {
                ["x-amz-meta-originalname"] = fileName,
                ["x-amz-meta-extension"] = Path.GetExtension(fileName),
            }
        };

        return await _amazonS3.PutObjectAsync(putObjectRequest);
    }

    public async Task<GetObjectResponse?> GetFileAsync(string path)
    {
        try
        {
            var getObjectRequest = new GetObjectRequest
            {
                BucketName = _settings.Bucket,
                Key = $"{FILES_FOLDER}/{path}",
            };

            return await _amazonS3.GetObjectAsync(getObjectRequest);
        }
        catch (AmazonS3Exception ex) when (ex.Message is KEY_NOT_EXISTS_EXCEPTION_MESSAGE)
        {
            return null;
        }
    }

    public async Task<DeleteObjectResponse> DeleteFileAsync(string path)
    {
        try
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _settings.Bucket,
                Key = $"{FILES_FOLDER}/{path}",
            };

            return await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
        }
        catch (AmazonS3Exception ex) when (ex.Message is KEY_NOT_EXISTS_EXCEPTION_MESSAGE)
        {
            return null;
        }
    }
}