using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestQ.Application.Dtos.FundosImobiliarios
{
    public class SegmentoAnbimaDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo de {0} deve ter no mínimo 3 e no máximo 100 caracteres.")]
        public string Descricao { get; set; }
        public IEnumerable<FundoImobiliarioDto> FundosImobiliarios { get; set; }
    }
}