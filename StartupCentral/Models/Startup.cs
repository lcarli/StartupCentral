using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StartupCentral.Models
{
    public class Startup
    {
        public Guid ID { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string msa { get; set; }
        public string BizSparkID { get; set; }
        public List<Contato> contatos { get; set; }
        public Benefício benefício { get; set; }
        public Status status { get; set; }
        public double ConsumoMes { get; set; }
        public double ConsumoAcumulado { get; set; }
        public double ConsumoPago { get; set; }
    }

    public class StartupDBContext : DbContext
    {
        public DbSet<Startup> Startup { get; set; }
    }
}