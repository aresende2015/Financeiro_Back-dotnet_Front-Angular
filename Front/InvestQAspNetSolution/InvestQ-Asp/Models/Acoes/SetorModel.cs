using System.ComponentModel.DataAnnotations;

namespace InvestQ_Asp.Models.Acoes
{
    public class SetorModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe a descri��o")]
        public string Descricao { get; set; }
    }
}