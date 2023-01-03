namespace UI.Models;

public class UpdateComixViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string Author { get; set; }
    public int YearOfPublish { get; set; }
    public IFormFile Photo { get; set; }
    public IFormFile DocFile { get; set; }

}
