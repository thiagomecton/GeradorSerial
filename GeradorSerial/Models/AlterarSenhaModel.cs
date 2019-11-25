using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GeradorSerial.Models
{
    public class AlterarSenhaModel : IValidatableObject
    {
        [Required]
        [DisplayName("Senha Atual")]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Nova Senha")]
        public string NovaSenha { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Confirma Senha")]
        public string ConfirmaNovaSenha { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NovaSenha != ConfirmaNovaSenha)
            {
                yield return new ValidationResult("Nova senha não confere com a confirmação", new string[] { nameof(NovaSenha) });
            }
        }
    }
}