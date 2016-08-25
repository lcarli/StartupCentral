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
    public class Aceleradora
    {
        [Required]
        public string nome { get; set; }
        [Key]
        public Guid ID { get; set; }
        public virtual Endereço Endereço { get; set; }
        [Required]
        public virtual Benefício Benefício { get; set; }
        [Required]
        public ICollection<Contato> Contatos { get; set; }
    }
}