using System.ComponentModel.DataAnnotations;

namespace InvestQ_Asp.Models.Acoes
{
    public class SetorModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe a descrição")]
        public string Descricao { get; set; }
    }
}