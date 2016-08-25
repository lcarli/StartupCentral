using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StartupCentral.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string nome { get; set; }
        public string alias { get; set; }
    }

    public class UserDBContext : DbContext
    {
        public DbSet<User> User { get; set; }
    }
}