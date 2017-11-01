using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Models;
using Daily.Hours.Web.Services;

namespace Daily.Hours.Web.Controllers
{
    public class UserController : ApiController
    {
        private UserService _userService = new UserService();

        [HttpPut]
        public UserModel Create(UserModel user)
        {
            return _userService.Create(user);
        }

        [HttpPost]
        public UserModel Update(UserModel user)
        {
            return _userService.Update(user);
        }

        [HttpDelete]
        public bool Delete(int userId)
        {
            return _userService.Delete(userId);
        }

        [HttpGet]
        public Task<UserModel>Get(int userId)
        {
            return _userService.Get(userId);
        }

        [HttpPost]
        public UserModel Login(UserModel userModel)
        {
            return _userService.Login(userModel.UserName, userModel.Password);
        }

        [HttpPost]
        public UserModel Register(UserModel user)
        {
            return _userService.Create(user);
        }
    }
}
