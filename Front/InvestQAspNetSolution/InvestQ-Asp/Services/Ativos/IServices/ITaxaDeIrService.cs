using InvestQ_Asp.Models.Ativos;
using InvestQ_Asp.Models;

namespace InvestQ_Asp.Services.Ativos.IServices
{
    public interface ITaxaDeIrService
    {
        Task<IEnumerable<TaxaDeIrModel>> FindAllTaxaDeIr(string token);
        Task<TaxaDeIrModel> FindTaxaDeIrById(Guid id, string token);
        Task<TaxaDeIrModel> CreateTaxaDeIr(TaxaDeIrModel model, string token);
        Task<TaxaDeIrModel> UpdateTaxaDeIr(TaxaDeIrModel model, Guid id, string token);
        Task<MensagemModel> DeleteTaxaDeIrById(Guid id, string token);
    }
}
