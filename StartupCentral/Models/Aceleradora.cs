using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StartupCentral.Models;
using System.Data.Entity;

namespace StartupCentral.Models
{
    public class Aceleradora
    {
        public string nome { get; set; }

        public Guid ID { get; set; }

        public Endereço Endereço { get; set; }

        public Benefício Benefício { get; set; }

        public Contato Contato { get; set; }
    }

    public class AceleradoraDBContext : DbContext
    {
        public DbSet<Aceleradora> Aceleradora { get; set; }
    }
}