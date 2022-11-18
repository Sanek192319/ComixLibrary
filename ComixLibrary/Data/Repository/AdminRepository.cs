namespace Data.Repository;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(ComixLibContext context) : base(context) { }
}
