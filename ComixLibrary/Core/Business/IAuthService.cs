namespace Core.Business;

public interface IAuthService
{
    Task Auth(string login, string pass);
    Task Login(string login, string pass);
}
