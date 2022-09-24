using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Dtos.Enum;
using InvestQ.Application.Interfaces.Ativos;
using InvestQ.Data.Interfaces.Ativos;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Application.Services.Ativos
{
    public class TaxaDeNegociacaoService : ITaxaDeNegociacaoService
    {
        private readonly ITaxaDeNegociacaoRepo _taxaDeNegociacaoRepo;
        private readonly IMapper _mapper;

        public TaxaDeNegociacaoService(ITaxaDeNegociacaoRepo taxaDeNegociacaoRepo,
                                       IMapper mapper)
        {
            _taxaDeNegociacaoRepo = taxaDeNegociacaoRepo;
            _mapper = mapper;
        }
        public async Task<TaxaDeNegociacaoDto> AdicionarTaxaDeNegociacao(TaxaDeNegociacaoDto model)
        {
            var taxaDeNegociacao = _mapper.Map<TaxaDeNegociacao>(model);

            if( await _taxaDeNegociacaoRepo.GetTaxaDeNegociacaoByIdAsync(taxaDeNegociacao.Id) == null)
            {
                var taxaDeNegociacaoVigente = await _taxaDeNegociacaoRepo
                                                        .GetTaxaDeNegociacaoByTipoDeAtivoVigenteAsync
                                                            (taxaDeNegociacao.TipoDeAtivo);
                
                if (taxaDeNegociacaoVigente != null) {

                    if (taxaDeNegociacao.DataInicio.Date <= taxaDeNegociacaoVigente.DataInicio.Date)
                        throw new Exception("A Taxa de Negociação tem que ter vigência superior a que já existe.");

                    taxaDeNegociacaoVigente.DataFim = taxaDeNegociacao.DataInicio.AddDays(-1);

                    _taxaDeNegociacaoRepo.Atualizar(taxaDeNegociacaoVigente);
                }

                _taxaDeNegociacaoRepo.Adicionar(taxaDeNegociacao);

                if (await _taxaDeNegociacaoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _taxaDeNegociacaoRepo.GetTaxaDeNegociacaoByIdAsync(taxaDeNegociacao.Id);

                    return _mapper.Map<TaxaDeNegociacaoDto>(retorno);
                }
            }

            return null;
        }

        public async Task<bool> DeletarTaxaDeNegociacao(Guid taxaDeNegociacaoId)
        {
            var taxaDeNegociacao = await _taxaDeNegociacaoRepo.GetTaxaDeNegociacaoByIdAsync(taxaDeNegociacaoId);

            if (taxaDeNegociacao == null)
                throw new Exception("A Taxa de Negociação que tentou deletar não existe.");

            if (taxaDeNegociacao.DataFim != null)
                throw new Exception("Só é permitido excluir a Taxa de Negociação vigente.");

            var taxaDeNegociacaoAnterior = await _taxaDeNegociacaoRepo
                                                    .GetTaxaDeNegociacaoByTipoDeAtivoComMaxDataFimAsync
                                                        (taxaDeNegociacao.TipoDeAtivo);

            if (taxaDeNegociacaoAnterior != null) {
                taxaDeNegociacaoAnterior.DataFim = null;

                _taxaDeNegociacaoRepo.Atualizar(taxaDeNegociacaoAnterior);
            }

            _taxaDeNegociacaoRepo.Deletar(taxaDeNegociacao);

            return await _taxaDeNegociacaoRepo.SalvarMudancasAsync();
        }

        public async Task<TaxaDeNegociacaoDto[]> GetAllTaxaDeNegociacaoAsync()
        {
            try
            {
                var taxasDeNegociacoes = await _taxaDeNegociacaoRepo.GetAllTaxaDeNegociacaoAsync();

                if (taxasDeNegociacoes == null) return null;

                return _mapper.Map<TaxaDeNegociacaoDto[]>(taxasDeNegociacoes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TaxaDeNegociacaoDto> GetTaxaDeNegociacaoByIdAsync(Guid id)
        {
            try
            {
                var taxaDeNegociacao = await _taxaDeNegociacaoRepo.GetTaxaDeNegociacaoByIdAsync(id);

                if (taxaDeNegociacao == null) return null;

                return _mapper.Map<TaxaDeNegociacaoDto>(taxaDeNegociacao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TaxaDeNegociacaoDto> GetTaxaDeNegociacaoByTipoDeAtivoAsync(TipoDeAtivoDto tipoDeAtivoDto, DateTime dataDeNegociacao)
        {
            try
            {
                var tipoDeAtivo = _mapper.Map<TipoDeAtivo>(tipoDeAtivoDto);

                var taxaDeNegociacao = await _taxaDeNegociacaoRepo.GetTaxaDeNegociacaoByTipoDeAtivoAsync(tipoDeAtivo, dataDeNegociacao);

                if (taxaDeNegociacao == null) return null;

                var RetornoDto = _mapper.Map<TaxaDeNegociacaoDto>(taxaDeNegociacao);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarTaxaDeNegociacao(TaxaDeNegociacaoDto model)
        {
            if (model != null)
            {
                var taxaDeNegociacao = _mapper.Map<TaxaDeNegociacao>(model);

                taxaDeNegociacao.Inativar();
                _taxaDeNegociacaoRepo.Atualizar(taxaDeNegociacao);
                return await _taxaDeNegociacaoRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarTaxaDeNegociacao(TaxaDeNegociacaoDto model)
        {
            if (model != null)
            {
                var taxaDeNegociacao = _mapper.Map<TaxaDeNegociacao>(model);

                taxaDeNegociacao.Reativar();
                _taxaDeNegociacaoRepo.Atualizar(taxaDeNegociacao);
                return await _taxaDeNegociacaoRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}