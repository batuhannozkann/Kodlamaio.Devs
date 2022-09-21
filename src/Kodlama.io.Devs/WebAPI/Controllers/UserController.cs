using Application.Features.Developers.Commands.Login;
using Application.Features.Developers.Commands.Register;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand registerCommand)
        {
            AccessToken accesToken = await Mediator.Send(registerCommand);
            return Created("", accesToken);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand loginCommand)
        {
            AccessToken accessToken = await Mediator.Send(loginCommand);
            return Created("", accessToken);
        }
    }
}
