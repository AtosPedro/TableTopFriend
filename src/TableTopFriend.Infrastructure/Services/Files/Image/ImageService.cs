using System.Net;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateUser.ValueObjects;

namespace TableTopFriend.Infrastructure.Services.Files.Images;

public class ImageService : IImageService
{
    private readonly IFileStorageService _fileStorageService;
    public ImageService(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task<bool> UploadImage(UserId userId, string key, IFormFile file)
    {
        string filePath = $"images/{userId.Value}/{key}";
        using var outStream = new MemoryStream();
        using (var image = await Image.LoadAsync(file.OpenReadStream()))
        {
            image.Mutate(x => x.Resize(500, 500, LanczosResampler.Lanczos3));
            await image.SaveAsync(outStream, image.DetectEncoder(file.FileName));
        }

        var response = await _fileStorageService.UploadFileAsync(filePath, file.ContentType, file.FileName, outStream);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<Stream?> GetImage(UserId userId, string key)
    {
        string filePath = $"images/{userId.Value}/{key}";
        var response = await _fileStorageService.GetFileAsync(filePath);

        if (response is null)
            return null;

        return response.ResponseStream;
    }

    public async Task<bool> DeleteImage(UserId userId, string key)
    {
        string filePath = $"images/{userId.Value}/{key}";
        var response = await _fileStorageService.DeleteFileAsync(filePath);            
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}
