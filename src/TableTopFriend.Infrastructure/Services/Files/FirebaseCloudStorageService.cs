using Microsoft.Extensions.Options;
using TableTopFriend.Application.Common.Interfaces.Services;

namespace TableTopFriend.Infrastructure.Services.Files;

public class FirebaseCloudStorageService : ICloudStorageService
{
    private readonly CloudStorageServiceSettings _options;


    public FirebaseCloudStorageService(IOptions<CloudStorageServiceSettings> options)
    {
        _options = options.Value;
    }

    public async Task SaveFileAsync(
         string fileName,
         string folderName,
         Stream fileStream,
         CancellationToken cancellationToken = default)
    {
        try
        {
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteFileAsync(string fileName)
    {
        await Task.CompletedTask;
    }

    private async Task<object> AuthenticateAsync()
    {
    }
}
