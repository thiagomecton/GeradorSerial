using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeradorSerial.Models
{
    public class Chave
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        [MaxLength(8)]
        [Display(Name = "Código de Ativação")]
        public string CodigodeAtivacao { get; set; }

        [MaxLength(8)]
        [Display(Name = "Chave de Ativação")]
        public string ChavedeAtivacao { get; set; }

        public DateTime? Data { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Cidade/Estado")]
        public string CidadeEstado { get; set; }

        [Required]
        [MaxLength(50)]
        public string Posto { get; set; }

        [Required]
        [MaxLength(50)]
        public string Linha { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }
    }
}