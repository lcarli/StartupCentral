using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace StartupCentral.Models
{
    public class Status
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string nome { get; set; }
    }
}