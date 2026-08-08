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

        [HttpPost("delete")]
        public object deleteUser(DelUserDTO delUserDTO)
        {
            try
            {
                var response = _userRepo.deleteUser(delUserDTO.CustomerId, delUserDTO.LoggedInUser);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("id")]
        public object getUserById(String customerId)
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

        [HttpGet("list")]
        public object listUsers()
        {
            try
            {
                var dt = _userRepo.listUsers();
                return new Dictionary<string, object>{ { "Testing", dt } };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
