namespace Core.Options;

public class FileSettings
{
    public string FilePath { get; set; }
    public string PhotoPath { get; set; }
    public IEnumerable<string> FileExtensions { get; set; }
}