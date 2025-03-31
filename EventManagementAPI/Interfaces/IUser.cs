using EventManagementAPI.Models;

namespace EventManagementAPI.Interfaces
{
    public interface IUserService
    {
        string Login(string username, string password);
    }
}
