namespace Core.Data;

public interface IUnitOfWork
{
    public IComixRepository ComixRepository { get; }
    public IAdminRepository AdminRepository { get; }
}
