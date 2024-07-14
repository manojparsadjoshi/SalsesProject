using SalsesProject.Models;

namespace Sales.Services.User
{
    public interface IUserService
    {
        bool ValidateLogin(string userName, string password);
        bool RegisterUser(UserModel user);

        UserModel GetUserWithRole(string userName, string password);
    }
}
