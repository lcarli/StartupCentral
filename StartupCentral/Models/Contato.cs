using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupCentral.Models
{
    [Table("Contato")]
    public class Contato
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string nome { get; set; }
        public string telefone { get; set; }
        [Required(ErrorMessage = "Email não pode ser branco.")]
        public string email { get; set; }
        public string TipoDoContato { get; set; }
        public ICollection<Aceleradora> aceleradora { get; set; }
        public ICollection<Startupbs> startup { get; set; }

    }
}