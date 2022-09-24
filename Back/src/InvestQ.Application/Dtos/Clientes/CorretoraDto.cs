using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using Flunt.Notifications;
//using Flunt.Validations;

namespace InvestQ.Application.Dtos.Clientes
{
    public class CorretoraDto //: Notifiable<Notification>
    {
        public CorretoraDto(Guid id,
                            string descricao,
                            string imagen)
        {
            // var contract = new Contract<CorretoraDto>()
            //                     .IsNotNullOrEmpty(descricao, "Descricao", "A Descrição é obrigatória.")
            //                     .IsLowerThan(descricao, 50, "Descrição", "A Descrição deve ter no máximo 50 caracteres")
            //                     .IsGreaterThan(descricao, 3, "Descrição", "A Descrição deve ter no mínimo 3 caracteres");

            // AddNotifications(contract);
            
            Id = id;
            Descricao = descricao;
            Imagen = imagen;
        }
        
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        //[MinLength(3, ErrorMessage = "{0} deve ter no mínimo 3 caracteres.")]
        //[MaxLength(50, ErrorMessage = "{0} deve ter no máximo 50 caracteres.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O campo de {0} deve ter no mínimo 3 e no máximo 50 caracteres.")]
        public string Descricao { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[RegularExpression(@".*\.(gif|jpe?g|bmp|png|jfif)$",  ErrorMessage = "Não é uma imagem válida. (gif, jpg, jpeg, jfif, bmp ou png")]
        public string Imagen { get; set; }
        public IEnumerable<CarteiraDto> Carteiras { get; set; }
    }
}