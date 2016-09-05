using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;

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
        public DbSet<Log> logs { get; set; }

        public StartupDBContext()
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StartupDBContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Detecta as alterações existentes na instância corrente do DbContext.
            this.ChangeTracker.DetectChanges();
            // Identifica as entidades que devem gerar registros em log.
            var entries = DetectEntries();
            // Cria lista para armazenamento temporário dos registros em log.
            List<Log> logs = new List<Log>(entries.Count());
            // Varre as entidades que devem gerar registros em log.
            foreach (var entry in entries)
            {
                // Cria novo registro de log.
                Log newLog = GetLog(entry);

                if (newLog != null)
                    logs.Add(newLog);
            }
            // Adiciona os registros de log na fonte de dados.
            foreach (var item in logs)
            {
                this.Entry(item).State = EntityState.Added;
            }
            // Persiste as informações na fonte de dados.
            return base.SaveChanges();
        }

        /// <summary>
        /// Identifica quais entidades devem ser gerar registros de log.
        /// </summary>
        private IEnumerable<DbEntityEntry> DetectEntries()
        {
            return ChangeTracker.Entries().Where(e => (e.State == EntityState.Modified ||
                                                        e.State == EntityState.Added ||
                                                        e.State == EntityState.Deleted) &&
                                                        e.Entity.GetType() != typeof(Log));
        }

        /// <summary>
        /// Cria os registros de log.
        /// </summary>
        private Log GetLog(DbEntityEntry entry)
        {

            Log returnValue = null;

            if (entry.State == EntityState.Added)
            {
                returnValue = GetInsertLog(entry);
            }
            else if (entry.State == EntityState.Modified)
            {
                returnValue = GetUpdateLog(entry);
            }
            else if (entry.State == EntityState.Deleted)
            {
                returnValue = GetDeleteLog(entry);
            }

            return returnValue;
        }

        private Log GetInsertLog(DbEntityEntry entry)
        {

            return Log.CreateInsertLog(entry.Entity);
        }

        private Log GetDeleteLog(DbEntityEntry entry)
        {

            return Log.CreateDeleteLog(entry.Entity);
        }

        private Log GetUpdateLog(DbEntityEntry entry)
        {

            object originalValue = null;

            if (entry.OriginalValues != null)
                originalValue = entry.OriginalValues.ToObject();
            else
                originalValue = entry.GetDatabaseValues().ToObject();

            return Log.CreateUpdateLog(originalValue, entry.Entity);
        }
    }
}