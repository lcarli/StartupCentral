using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StartupCentral.Models
{
    public class Status
    {
        public Guid ID { get; set; }
        public string nome { get; set; }
    }

    public class StatusDBContext : DbContext
    {
        public DbSet<Status> Status { get; set; }
    }
}