using GeradorSerial.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeradorSerial.Models
{
    public class Usuario : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public virtual ICollection<Chave> Chaves { get; set; }

        public virtual ICollection<MasterKey> MasterKeys { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(11)]
        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        [StringLength(320, MinimumLength = 7)]
        [MaxLength(320)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required]
        public EnumTipoUsuario? Tipo { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Cpf == null)
            {
                yield return new ValidationResult("Campo obrigatório", new string[] { nameof(Cpf) });
            }
            else if (!Cpf.IsValidCpf())
            {
                yield return new ValidationResult("CPF inválido", new string[] { nameof(Cpf) });
            }
        }
    }
}