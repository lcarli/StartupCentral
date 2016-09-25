using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupCentral.Models
{
    public class Observacoes
    {
        [Key]
        public int ObservacoesId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Datetime { get; set; }

        public int StartupbsId { get; set; }

        public virtual Startupbs Startupbs { get; set; }

        public int AceleradoraId { get; set; }

        public virtual Aceleradora Aceleradora { get; set; }

        public string texto { get; set; }
    }
}