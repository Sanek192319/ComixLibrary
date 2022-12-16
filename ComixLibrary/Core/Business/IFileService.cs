namespace Core.Business;

public interface IFileService
{
    Task<string> UploadFile(Stream file, string filePath);
    Task<FileStream> GetFile(string filePath);
    Task DeleteFile(string filePath);
}
