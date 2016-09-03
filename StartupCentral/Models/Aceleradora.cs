using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StartupCentral.Models;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StartupCentral.Models
{
    [Table("Aceleradora")]
    public class Aceleradora
    {
        [Required]
        public string nome { get; set; }
        [Key]
        public Guid ID { get; set; }
        public virtual Endereço Endereço { get; set; }
        public virtual Benefício Benefício { get; set; }
        public virtual ICollection<Contato> Contatos { get; set; }
        public virtual ICollection<Startupbs> Startups { get; set; }
        public string Observacoes { get; set; }
    }
}