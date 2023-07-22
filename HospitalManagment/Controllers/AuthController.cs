using Application.Interfaces;
using Domain.Domain;
using Domain.DomainRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISqlUserRepo _userService;

        public AuthController(ISqlUserRepo userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.RegisterAsync(model);

            return Ok(new { Message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<ActionResult<DoctorWithToken>> Login(LoginReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResult = await _userService.LoginAsync(model);

            return Ok(loginResult);
        }
    }
}
