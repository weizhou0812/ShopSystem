using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSystem.IService;
using ShopSystem.ViewModels;

namespace ShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _iService;
        public LoginController(IUserService iUserService)
        {
            this._iService = iUserService;
        }
        [HttpPost("Auth")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _iService.Authenticate(model);
            if (response == null)
            {
                return BadRequest(new { message = "用户名密码不正确" });
            }
            return Ok(response);
        }
    }
}
