using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Dtos.Enum;
using InvestQ.Application.Interfaces.Ativos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaxaDeIRController : ControllerBase
    {
        private readonly ITaxaDeIRService _taxaDeIRService;
        public TaxaDeIRController(ITaxaDeIRService taxaDeIRService)
        {
            _taxaDeIRService = taxaDeIRService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var taxasDeIR = await _taxaDeIRService.GetAllTaxaDeIRAsync();

                 if (taxasDeIR == null) return NoContent();

                 return Ok(taxasDeIR);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos as Taxas De IR. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaxaDeIRById(Guid id)
        {
            try
            {
                 var taxaDeIR = await _taxaDeIRService.GetTaxaDeIRByIdAsync(id);

                 if (taxaDeIR == null) return NoContent();

                 return Ok(taxaDeIR);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a Taxa de IR com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tipoDeAtivo}/{dataDeNegociacao}")]
        public async Task<IActionResult> Get(TipoDeAtivoDto tipoDeAtivo, DateTime dataDeNegociacao) 
        {
            try
            {
                 var taxaDeIR = await _taxaDeIRService.GetTaxaDeIRByTipoDeAtivoAsync(tipoDeAtivo, dataDeNegociacao);

                 if (taxaDeIR == null) return NoContent();

                 return Ok(taxaDeIR);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a Taxa de IR do Tipo de Ativo na data de negociação. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(TaxaDeIRDto model) 
        {
            try
            {
                 var taxaDeIR = await _taxaDeIRService.AdicionarTaxaDeIR(model);
                 if (taxaDeIR == null) return BadRequest("Erro ao tentar adicionar a Taxa de IR.");

                 return Ok(taxaDeIR);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar a Taxa de IR. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var taxaDeIR = await _taxaDeIRService.GetTaxaDeIRByIdAsync(id);
                if (taxaDeIR == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar uma Taxa de IR que não existe.");

                if(await _taxaDeIRService.DeletarTaxaDeIR(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar a Taxa de IR.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar a Taxa de IR com id: ${id}. Erro: {ex.Message}");
            }
        } 
    }
}