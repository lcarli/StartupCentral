using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupCentral.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Nome não pode ser branco.")]
        public string nome { get; set; }
        [Required(ErrorMessage = "Alias não pode ser branco.")]
        public string alias { get; set; }
    }
}