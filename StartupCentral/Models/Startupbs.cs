using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupCentral.Models
{
    [Table("Startupbs")]
    public class Startupbs
    {
        [Key]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Nome não pode ser branco.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email não pode ser branco.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Microsoft Account não pode ser branco.")]
        public string MicrosoftAccount { get; set; }
        public string BizSparkID { get; set; }
        [Required(ErrorMessage = "Contato não pode ser branco.")]
        public ICollection<Contato> Contatos { get; set; }
        public virtual Benefício Benefício { get; set; }
        public virtual Aceleradora Aceleradora { get; set; }
        public virtual Status Status { get; set; }
        public double ConsumoMes { get; set; }
        public double ConsumoAcumulado { get; set; }
        public double ConsumoPago { get; set; }
    }

    public class StartupDBContext : DbContext
    {
        public DbSet<Startupbs> Startup { get; set; }
        public DbSet<Aceleradora> Aceleradora { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Endereço> Endereço { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Benefício> Benefício { get; set; }
        public DbSet<LogLogin> LogLogin { get; set; }
    }
}