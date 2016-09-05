using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StartupCentral.Models
{
    public enum UserAction
    {
        Salvou,
        Editou,
        Deletou,
        Consultou
    }

    public class GeneralLog
    {
        [Key]
        public int GeneralLogId { get; set; }
        public int UsuarioId { get; set; }
        public virtual User Usuario { get; set; }
        public DateTime Datetime { get; set; }
        public UserAction Action { get; set; }
        public string ObjectUsed { get; set; }
    }
}