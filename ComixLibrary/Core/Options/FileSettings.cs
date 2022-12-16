namespace Core.Options;

public class FileSettings
{
    public string Path { get; set; }
    public IEnumerable<string> FileExtensions { get; set; }
}