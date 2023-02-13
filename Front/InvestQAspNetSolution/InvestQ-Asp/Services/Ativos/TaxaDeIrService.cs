using InvestQ_Asp.Models;
using InvestQ_Asp.Models.Acoes;
using InvestQ_Asp.Models.Ativos;
using InvestQ_Asp.Services.Ativos.IServices;
using InvestQ_Asp.Utils;
using System.Net.Http.Headers;

namespace InvestQ_Asp.Services.Ativos
{
    public class TaxaDeIrService : ITaxaDeIrService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/taxadeir";
        public TaxaDeIrService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<TaxaDeIrModel>> FindAllTaxaDeIr(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<List<TaxaDeIrModel>>();
        }
        public async Task<TaxaDeIrModel> FindTaxaDeIrById(Guid id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{BasePath}/{id}");

            return await response.ReadContentAs<TaxaDeIrModel>();   
        }
        public async Task<TaxaDeIrModel> CreateTaxaDeIr(TaxaDeIrModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson(BasePath, model);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<TaxaDeIrModel>();
            else throw new Exception("Erro ao incluir a Taxa de IR!");
        }
        public async Task<TaxaDeIrModel> UpdateTaxaDeIr(TaxaDeIrModel model, Guid id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson(BasePath, model);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<TaxaDeIrModel>();
            else throw new Exception("Erro ao alterar a Taxa de IR!");
        }
        public async Task<MensagemModel> DeleteTaxaDeIrById(Guid id, string token)
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
