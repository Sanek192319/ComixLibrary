namespace Core.Domain;

public class Comix : BaseEntity, IComparable<Comix>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string Author { get; set; }
    public DateTime YearOfPublishing { get; set; }

    public int CompareTo(Comix? other)
    {
        if (other == null) return 1;
        if (other == this) return 0;
        return other.Name.CompareTo(Name);
    }
}
