﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupCentral.Models
{
    [Table("Contato")]
    public class Contato
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Nome { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Email não pode ser branco.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string TipoDoContato { get; set; }
        public virtual ICollection<Startupbs> Startup { get; set; }
        public virtual ICollection<Aceleradora> Aceleradora { get; set; }
    }
}