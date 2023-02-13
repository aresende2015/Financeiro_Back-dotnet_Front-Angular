using InvestQ_Asp.Models;
using InvestQ_Asp.Models.Acoes;
using InvestQ_Asp.Services.Acoes.IServices;
using InvestQ_Asp.Utils;
using System.Net.Http.Headers;

namespace InvestQ_Asp.Services.Acoes
{
    public class SetorService : ISetorService
    {
        private readonly HttpClient _client;

        public const string BasePath = "api/setor";

        public SetorService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<SetorModel>> FindAllSetores(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<List<SetorModel>>();
        }
        public async Task<SetorModel> FindSetorById(Guid id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{id}");

            return await response.ReadContentAs<SetorModel>();
        }
        public async Task<SetorModel> FindSetorByDescricao(string descricao, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{descricao}");

            return await response.ReadContentAs<SetorModel>();
        }
        public async Task<SetorModel> CreateSetor(SetorModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson(BasePath, model);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<SetorModel>();
            else throw new Exception("Erro ao incluir o setor!");

        }
        public async Task<SetorModel> UpdateSetor(SetorModel model, Guid id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson(BasePath, model);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<SetorModel>();
            else throw new Exception("Erro ao alterar o setor!");
        }
        public async Task<MensagemModel> DeleteSetorById(Guid id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BasePath}/{id}");

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<MensagemModel>();
            //return true;
            else throw new Exception("Erro ao excluir o setor!");
        }
    }
}
