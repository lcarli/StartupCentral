using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StartupCentral.Models
{
    public class Endereço
    {
        public Guid ID { get; set; }
        public string rua { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cep { get; set; }
        public string estado { get; set; }
        public string país { get; set; }

    }

    public class EndereçoDBContext : DbContext
    {
        public DbSet<Endereço> Endereço { get; set; }
    }
}