using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StartupCentral.Models
{
    public class Benefício
    {
        public Guid ID { get; set; }

        public string nome { get; set; }
    }

    public class BenefícioDBContext : DbContext
    {
        public DbSet<Benefício> Benefício { get; set; }
    }
}