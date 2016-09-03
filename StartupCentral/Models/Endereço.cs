using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupCentral.Models
{
    [Table("Endereco")]
    public class Endereço
    {
        [Key]
        public int EnderecoId { get; set; }

        [Required(ErrorMessage = "Rua não pode ser branco.")]
        public string Rua { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        [DataType(DataType.PostalCode)]
        public string CEP { get; set; }

        [StringLength(2, ErrorMessage = "Estado deve conter apenas 2 letras. EX: SP", MinimumLength = 2)]
        public string Estado { get; set; }

        [StringLength(2, ErrorMessage = "País deve conter apenas 2 letras. EX: BR", MinimumLength = 2)]
        [Required(ErrorMessage = "País não pode ser branco.")]
        public string País { get; set; }
        
    }
}