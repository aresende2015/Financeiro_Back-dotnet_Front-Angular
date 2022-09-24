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
    public class TaxaDeNegociacaoController : ControllerBase
    {
        private readonly ITaxaDeNegociacaoService _taxaDeNegociacaoService;

        public TaxaDeNegociacaoController(ITaxaDeNegociacaoService taxaDeNegociacaoService)
        {
            _taxaDeNegociacaoService = taxaDeNegociacaoService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var taxasDeNegociacoes = await _taxaDeNegociacaoService.GetAllTaxaDeNegociacaoAsync();

                 if (taxasDeNegociacoes == null) return NoContent();

                 return Ok(taxasDeNegociacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos as Taxas De Negociações. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaxaDeNegociacaoById(Guid id)
        {
            try
            {
                 var taxaDeNegociacao = await _taxaDeNegociacaoService.GetTaxaDeNegociacaoByIdAsync(id);

                 if (taxaDeNegociacao == null) return NoContent();

                 return Ok(taxaDeNegociacao);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a Taxa de Negociação com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tipoDeAtivo}/{dataDeNegociacao}")]
        public async Task<IActionResult> Get(TipoDeAtivoDto tipoDeAtivo, DateTime dataDeNegociacao) 
        {
            try
            {
                 var taxaDeNegociacao = await _taxaDeNegociacaoService.GetTaxaDeNegociacaoByTipoDeAtivoAsync(tipoDeAtivo, dataDeNegociacao);

                 if (taxaDeNegociacao == null) return NoContent();

                 return Ok(taxaDeNegociacao);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a Taxa de Negociação do Tipo de Ativo na data de negociação. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(TaxaDeNegociacaoDto model) 
        {
            try
            {
                 var taxaDeNegociacao = await _taxaDeNegociacaoService.AdicionarTaxaDeNegociacao(model);
                 if (taxaDeNegociacao == null) return BadRequest("Erro ao tentar adicionar a Taxa de Negociação.");

                 return Ok(taxaDeNegociacao);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar a Taxa de Negociação. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var taxaDeNegociacao = await _taxaDeNegociacaoService.GetTaxaDeNegociacaoByIdAsync(id);
                if (taxaDeNegociacao == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar uma Taxa de Negociação que não existe.");

                if(await _taxaDeNegociacaoService.DeletarTaxaDeNegociacao(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar a Taxa de Negociação.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar a Taxa de Negociação com id: ${id}. Erro: {ex.Message}");
            }
        }  
    }
}