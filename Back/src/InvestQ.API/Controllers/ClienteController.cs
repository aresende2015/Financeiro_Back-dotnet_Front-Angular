using System;
using System.Threading.Tasks;
using InvestQ.API.Extensions;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Application.Interfaces.Clientes;
using InvestQ.Application.Interfaces.Identity;
using InvestQ.Data.Paginacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IUserService _userService;

        public ClienteController(IClienteService clienteService,
                                 IUserService userService)
        {
            _clienteService = clienteService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams) 
        {
            try
            {
                 var clientes = await _clienteService.GetAllClientesAsync(User.GetUserId(), pageParams, true);

                 if (clientes == null) return NoContent();

                 Response.AddPagination(clientes.CurrentPage, clientes.PageSize, clientes.TotalCount, clientes.TotalPages);

                 return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Clientes. Erro: {ex.Message}");
            }
        }

        [HttpGet("usuariologado/{usuariologado}")]
        public async Task<IActionResult> Get(string usuarioLogado) 
        {
            try
            {
                 var clientes = await _clienteService.GetAllClientesUserAsync(User.GetUserId(), true);

                 if (clientes == null) return NoContent();

                 return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os cliente do usuário logado. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                 var cliente = await _clienteService.GetClienteByIdAsync(User.GetUserId(), id, true);

                 if (cliente == null) return NoContent();

                 return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Cliente com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto model) 
        {
            try
            {
                 var cliente = await _clienteService.AdicionarCliente(User.GetUserId(), model);
                 if (cliente == null) return BadRequest("Erro ao tentar adicionar o Cliente.");

                 return Ok(cliente);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status417ExpectationFailed, 
                    $"Erro ao tentar adicionar um Cliente. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ClienteDto model)
        {
            try
            {
                 var cliente = await _clienteService.AtualizarCliente(User.GetUserId(), id, model);
                 if (cliente == null) return NoContent();

                 return Ok(cliente);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar um Cliente com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(User.GetUserId(), id,false);
                if (cliente == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Cliente que não existe.");

                if(await _clienteService.DeletarCliente(User.GetUserId(), id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Cliente.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status417ExpectationFailed, 
                    $"Erro ao tentar deletar o Cliente com id: ${id}. Erro: {ex.Message}");
            }
        }

    }
}