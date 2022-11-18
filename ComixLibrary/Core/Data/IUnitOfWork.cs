namespace Core.Data;

public interface IUnitOfWork : IDisposable
{
    public IComixRepository ComixRepository { get; }
    public IAdminRepository AdminRepository { get; }
}
