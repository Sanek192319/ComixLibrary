namespace Data;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IAdminRepository adminRepository, IComixRepository comixRepository)
    {
        AdminRepository = adminRepository;
        ComixRepository = comixRepository;
    }

    public IAdminRepository AdminRepository { get; }
    public IComixRepository ComixRepository { get; }
}