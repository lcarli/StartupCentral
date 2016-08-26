using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupCentral.Models
{
    [Table("Endereço")]
    public class Endereço
    {
        [Key]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Rua não pode ser branco.")]
        public string rua { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cep { get; set; }
        public string estado { get; set; }
        [Required(ErrorMessage = "País não pode ser branco.")]
        public string país { get; set; }

    }
}