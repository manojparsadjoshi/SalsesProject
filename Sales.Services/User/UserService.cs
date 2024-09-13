using Sales.Db;
using SalsesProject.Models;

namespace Sales.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public UserModel GetUserWithRole(string userName, string password)
        {
            UserModel checkdata = new UserModel();
            checkdata = _context.Users.FirstOrDefault(x => x.Username == userName && x.Password == password);
            return checkdata;
        }

        public bool RegisterUser(UserModel userdata)
        {
            bool existUser = _context.Users.Count(x => x.Username == userdata.Username) > 0;
            if (!existUser)
            {
                _context.Users.Add(userdata);
                _context.SaveChanges();
                return true;            
            }
            return false;
        }

        public bool ValidateLogin(string userName, string password)
        {
            bool validUser = _context.Users.Any(x => x.Username == userName && x.Password == password);
            return validUser;
        }
    }
}
