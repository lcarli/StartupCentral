using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StartupCentral.Models
{
    public class LogLogin
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime datetime { get; set; }
        public virtual User user { get; set; }
    }
}