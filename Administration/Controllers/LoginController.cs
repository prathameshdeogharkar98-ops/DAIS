using Administration.Entities.DTOs;
using Administration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepo _loginRepo;

        public LoginController(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }

        [Authorize]
        [HttpGet("test")]
        public object test()
        {
            try
            {
                return "Temp";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public object userLogin(LoginDTO loginDTO)
        {
            try
            {
                var response = _loginRepo.loginUser(loginDTO);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
