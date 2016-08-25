using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace StartupCentral.Models
{
    public class Endereço
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string rua { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cep { get; set; }
        public string estado { get; set; }
        [Required]
        public string país { get; set; }

    }
}