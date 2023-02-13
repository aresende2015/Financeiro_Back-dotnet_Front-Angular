using InvestQ_Asp.Models;
using InvestQ_Asp.Models.Acoes;

namespace InvestQ_Asp.Services.Acoes.IServices
{
    public interface ISetorService
    {
        Task<IEnumerable<SetorModel>> FindAllSetores(string token);
        Task<SetorModel> FindSetorById(Guid id, string token);
        Task<SetorModel> FindSetorByDescricao(string descricao, string token);
        Task<SetorModel> CreateSetor(SetorModel model, string token);
        Task<SetorModel> UpdateSetor(SetorModel model, Guid id, string token);
        Task<MensagemModel> DeleteSetorById(Guid id, string token);

    }
}