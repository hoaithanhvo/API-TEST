using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;

namespace MCSAndroidAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public AuthenticateController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
        {
            var response = await _repository.Authenticate.AuthenticateAsync(model);

            return Generation.GenerateJson(response);
        }
        [HttpPost("logout")]
        
        public IActionResult Logout()
        {
            return Ok();
        }

    }
}
