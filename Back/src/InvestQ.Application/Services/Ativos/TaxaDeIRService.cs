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
    public class TaxaDeIRService : ITaxaDeIRService
    {
        private readonly ITaxaDeIRRepo _taxaDeIRRepo;
        private readonly IMapper _mapper;
        public TaxaDeIRService(ITaxaDeIRRepo taxaDeIRRepo,
                               IMapper mapper)
        {
            _taxaDeIRRepo = taxaDeIRRepo;
            _mapper = mapper;
        }

        public async Task<TaxaDeIRDto> AdicionarTaxaDeIR(TaxaDeIRDto model)
        {
            var taxaDeIR = _mapper.Map<TaxaDeIR>(model);

            if( await _taxaDeIRRepo.GetTaxaDeIRByIdAsync(taxaDeIR.Id) == null)
            {
                var taxaDeIRVigente = await _taxaDeIRRepo
                                                        .GetTaxaDeIRByTipoDeAtivoVigenteAsync
                                                            (taxaDeIR.TipoDeAtivo);
                
                if (taxaDeIRVigente != null) {

                    if (taxaDeIR.DataInicio.Date <= taxaDeIRVigente.DataInicio.Date)
                        throw new Exception("A Taxa de IR tem que ter vigência superior a que já existe.");

                    taxaDeIRVigente.DataFim = taxaDeIR.DataInicio.AddDays(-1);

                    _taxaDeIRRepo.Atualizar(taxaDeIRVigente);
                }

                _taxaDeIRRepo.Adicionar(taxaDeIR);

                if (await _taxaDeIRRepo.SalvarMudancasAsync())
                {
                    var retorno = await _taxaDeIRRepo.GetTaxaDeIRByIdAsync(taxaDeIR.Id);

                    return _mapper.Map<TaxaDeIRDto>(retorno);
                }
            }

            return null;
        }

        public async Task<bool> DeletarTaxaDeIR(Guid taxaDeIRId)
        {
            var taxaDeIR = await _taxaDeIRRepo.GetTaxaDeIRByIdAsync(taxaDeIRId);

            if (taxaDeIR == null)
                throw new Exception("A Taxa de IR que tentou deletar não existe.");

            if (taxaDeIR.DataFim != null)
                throw new Exception("Só é permitido excluir a Taxa de IR vigente.");

            var taxaDeIRAnterior = await _taxaDeIRRepo
                                                    .GetTaxaDeIRByTipoDeAtivoComMaxDataFimAsync
                                                        (taxaDeIR.TipoDeAtivo);

            if (taxaDeIRAnterior != null) {
                taxaDeIRAnterior.DataFim = null;

                _taxaDeIRRepo.Atualizar(taxaDeIRAnterior);
            }

            _taxaDeIRRepo.Deletar(taxaDeIR);

            return await _taxaDeIRRepo.SalvarMudancasAsync();
        }

        public async Task<TaxaDeIRDto[]> GetAllTaxaDeIRAsync()
        {
            try
            {
                var taxasDeIR = await _taxaDeIRRepo.GetAllTaxaDeIRAsync();

                if (taxasDeIR == null) return null;

                return _mapper.Map<TaxaDeIRDto[]>(taxasDeIR);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TaxaDeIRDto> GetTaxaDeIRByIdAsync(Guid id)
        {
            try
            {
                var taxaDeIR = await _taxaDeIRRepo.GetTaxaDeIRByIdAsync(id);

                if (taxaDeIR == null) return null;

                return _mapper.Map<TaxaDeIRDto>(taxaDeIR);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TaxaDeIRDto> GetTaxaDeIRByTipoDeAtivoAsync(TipoDeAtivoDto tipoDeAtivoDto, DateTime dataDeNegociacao)
        {
            try
            {
                var tipoDeAtivo = _mapper.Map<TipoDeAtivo>(tipoDeAtivoDto);

                var taxaDeIR = await _taxaDeIRRepo.GetTaxaDeIRByTipoDeAtivoAsync(tipoDeAtivo, dataDeNegociacao);

                if (taxaDeIR == null) return null;

                var RetornoDto = _mapper.Map<TaxaDeIRDto>(taxaDeIR);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarTaxaDeIR(TaxaDeIRDto model)
        {
            if (model != null)
            {
                var taxaDeIR = _mapper.Map<TaxaDeIR>(model);

                taxaDeIR.Inativar();
                _taxaDeIRRepo.Atualizar(taxaDeIR);
                return await _taxaDeIRRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarTaxaDeIR(TaxaDeIRDto model)
        {
            if (model != null)
            {
                var taxaDeIR = _mapper.Map<TaxaDeIR>(model);

                taxaDeIR.Reativar();
                _taxaDeIRRepo.Atualizar(taxaDeIR);
                return await _taxaDeIRRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}