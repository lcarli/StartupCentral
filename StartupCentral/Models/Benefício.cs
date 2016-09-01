using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupCentral.Models
{
    [Table("Benefício")]
    public class Benefício
    {     
        [Key]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Nome não pode ser branco.")]
        public string Nome { get; set; }
    }
}