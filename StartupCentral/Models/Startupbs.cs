using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StartupCentral.Models
{
    //[Table("Startupbs")]
    public class Startupbs
    {
        public Startupbs()
        {
            this.Contatos = new HashSet<Contato>();
        }


        [Key]
        public int StartupbsId { get; set; }

        [Required(ErrorMessage = "Nome não pode ser branco."), Display(Name = "Nome*")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email não pode ser branco."), Display(Name = "Email*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Microsoft Account não pode ser branco."), Display(Name = "Microsoft Account*")]
        public string MicrosoftAccount { get; set; }

        [Display(Name = "BizSpark ID")]
        public string BizSparkID { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; }

        [Display(Name = "Benefício")]
        public int BeneficioId { get; set; }

        public virtual Beneficio Beneficio { get; set; }

        [Display(Name = "Aceleradora")]
        public int AceleradoraId { get; set; }

        public virtual Aceleradora Aceleradora { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }


        [Display(Name = "Consumo do Mês")]
        public double ConsumoMes { get; set; }


        [Display(Name = "Consumo Acumulado do Ano")]
        public double ConsumoAcumulado { get; set; }

        //[DisplayFormat(DataFormatString = "{0,c}")]
        [Display(Name = "Consumo Pago")]
        public double ConsumoPago { get; set; }

        public string Observacoes { get; set; }

        public string Owner { get; set; }
    }

    public class StartupDBContext : DbContext
    {
        public DbSet<Startupbs> Startup { get; set; }
        public DbSet<Aceleradora> Aceleradora { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Endereco> Endereço { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Beneficio> Benefício { get; set; }
        public DbSet<LogLogin> LogLogin { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<GeneralLog> GeneralLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}