using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StartupCentral.Models
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}