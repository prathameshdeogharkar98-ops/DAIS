using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAccessManagement.Entities.DTOs;
using UserAccessManagement.Interfaces;

namespace UserAccessManagement.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        public object createOrUpdateUser(UserDTO userDTO)
        {
            try
            {
                var response = _userRepo.createOrUpdateUser(userDTO);
                return response;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        public object Test()
        {
            try
            {
                
                return new Dictionary<string,string>{{"Testing", "Working" }};
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
