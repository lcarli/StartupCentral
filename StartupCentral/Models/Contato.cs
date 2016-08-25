using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace StartupCentral.Models
{
    public class Contato
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string nome { get; set; }
        public string telefone { get; set; }
        [Required]
        public string email { get; set; }
        public ICollection<Aceleradora> aceleradora { get; set; }
        public ICollection<Startup> startup { get; set; }

    }
}