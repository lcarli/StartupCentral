using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace StartupCentral.Models
{
    public class Startup
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string msa { get; set; }
        public string BizSparkID { get; set; }
        [Required]
        public ICollection<Contato> contatos { get; set; }
        public virtual Benefício benefício { get; set; }
        public virtual Status status { get; set; }
        public double ConsumoMes { get; set; }
        public double ConsumoAcumulado { get; set; }
        public double ConsumoPago { get; set; }
    }

    public class StartupDBContext : DbContext
    {
        public DbSet<Startup> Startup { get; set; }
        public DbSet<Aceleradora> Aceleradora { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Endereço> Endereço { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Benefício> Benefício { get; set; }
    }
}