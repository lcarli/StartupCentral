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
        public Aceleradora()
        {
            this.Startups = new List<Startupbs>();
            this.Contatos = new HashSet<Contato>();
            this.Observacoes = new List<Observacoes>();
        }

        [Required(ErrorMessage = "Nome não pode ser branco.")]
        public string Nome { get; set; }

        [Key]
        public int AceleradoraId { get; set; }


        public virtual Endereco Endereco { get; set; }

        public int BeneficioId { get; set; }

        public virtual Beneficio Beneficio { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; }

        public virtual ICollection<Startupbs> Startups { get; set; }

        public virtual ICollection<Observacoes> Observacoes { get; set; }
    }
}