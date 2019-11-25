using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeradorSerial.Models
{
    public class MasterKey
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public DateTime Data { get; set; }

        [MaxLength(8)]
        public string ChaveMaster { get; set; }
    }
}