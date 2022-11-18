namespace Data.Repository;

public class ComixRepository : BaseRepository<Comix>, IComixRepository
{
    public ComixRepository(ComixLibContext context) : base(context) { }
}
