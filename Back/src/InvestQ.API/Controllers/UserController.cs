using System;
using System.Threading.Tasks;
using InvestQ.API.Extensions;
using InvestQ.Application.Dtos.Identity;
using InvestQ.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService,
                              ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser() 
        {
            try
            {
                var username = User.GetUserName();

                var user = await _userService.GetUserByUsernameAsync(username);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar Usuário. Erro {ex.Message}");
            }

        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto) 
        {
            try
            {
                if (await _userService.UserExists(userDto.Username))
                    return BadRequest("Usuário já existe.");

                var user = await _userService.CreateUserAsync(userDto);

                if (user != null)                    
                    return Ok(new 
                        {
                            username = user.Username,
                            primeiroNome = user.PrimeiroNome,
                            token = _tokenService.CreateToken(user).Result
                        });

                return BadRequest("Usuário não criado, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar registrar Usuário. Erro {ex.Message}");
            }

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto) 
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(userLoginDto.Username);

                if (user == null) return Unauthorized("Usuário ou Senha está errado.");

                var result = await _userService.CheckUserPasswordAsync(user, userLoginDto.Password);

                if (!result.Succeeded) return Unauthorized("Usuário ou Senha está errado.");

                return Ok(new 
                {
                    username = user.Username,
                    primeiroNome = user.PrimeiroNome,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar realizar Login. Erro {ex.Message}");
            }

        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto) 
        {
            try
            {
                if (userUpdateDto.Username != User.GetUserName())
                    return Unauthorized("Usuário Inválido");

                var user = await _userService.GetUserByUsernameAsync(User.GetUserName());

                if (user == null) return Unauthorized("Usuário inválido.");

                var userReturn = await _userService.UpdateUser(userUpdateDto);

                if (userReturn == null)
                    return NoContent();

                return Ok(new 
                {
                    username = userReturn.Username,
                    primeiroNome = userReturn.PrimeiroNome,
                    token = _tokenService.CreateToken(userReturn).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar Usuário. Erro {ex.Message}");
            }

        }
        
    }
}