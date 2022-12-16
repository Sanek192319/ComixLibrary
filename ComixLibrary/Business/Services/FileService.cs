using Core.Business;
using Core.Options;

namespace Business.Services;

public class FileService : IFileService
{
    
    private readonly FileSettings _fileSettings;

    public FileService(FileSettings fileSettings)
    {
        _fileSettings = fileSettings;
    }

    public Task DeleteFile(string filePath)
    {
        return Task.Run(() => File.Delete(filePath));
    }

    public Task<FileStream> GetFile(string filePath)
    {
        return Task.FromResult(new FileStream(filePath, FileMode.OpenOrCreate));
    }

    public async Task<string> UploadFile(Stream file, string filePath)
    {
        var directoryPath = Path.GetDirectoryName(filePath);
        if (directoryPath is null)
        {
            return null;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
        await file.CopyToAsync(fileStream);

        return filePath;
    }
}
