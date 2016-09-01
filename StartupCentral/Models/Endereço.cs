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
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        [Required(ErrorMessage = "País não pode ser branco.")]
        public string País { get; set; }

    }
}