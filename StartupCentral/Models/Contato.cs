using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StartupCentral.Models
{
    public class Contato
    {
        public Guid ID { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public Aceleradora aceleradora { get; set; }
        public Startup startup { get; set; }

    }

    public class ContatoDBContext : DbContext
    {
        public DbSet<Contato> Contato { get; set; }
    }
}