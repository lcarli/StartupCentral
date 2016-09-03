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
        public int LogLoginId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime datetime { get; set; }

        public virtual User user { get; set; }
    }
}