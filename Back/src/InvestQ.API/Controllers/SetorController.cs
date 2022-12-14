using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Acoes;
using InvestQ.Application.Interfaces.Acoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SetorController : ControllerBase
    {
        private readonly ISetorService _setorService;
        public SetorController(ISetorService sertorService)
        {
            _setorService =sertorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var setores = await _setorService.GetAllSetoresAsync(true);

                 if (setores == null) return NoContent();

                 return Ok(setores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Setores. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                 var setor = await _setorService.GetSetorByIdAsync(id, true);

                 if (setor == null) return NoContent();

                 return Ok(setor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Setor com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(SetorDto model) 
        {
            try
            {
                 var setor = await _setorService.AdicionarSetor(model);
                 if (setor == null) return BadRequest("Erro ao tentar adicionar o Setor.");

                 return Ok(setor);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar um Setor. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, SetorDto model)
        {
            try
            {
                 var setor = await _setorService.AtualizarSetor(id, model);
                 if (setor == null) return NoContent();

                 return Ok(setor);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar um Setor com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var setor = await _setorService.GetSetorByIdAsync(id,false);
                if (setor == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Voc?? est?? tetando deletar um Setor que n??o existe.");

                if(await _setorService.DeletarSetor(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema n??o espec??fico ao tentar deletar o Setor.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o Setor com id: ${id}. Erro: {ex.Message}");
            }
        }
        
    }
}