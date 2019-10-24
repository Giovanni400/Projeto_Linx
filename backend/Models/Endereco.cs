using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class Endereco
    {
        public Endereco()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        public int IdEndereco { get; set; }
        [Column("Endereco")]
        [StringLength(255)]
        public string Endereco1 { get; set; }
        [StringLength(10)]
        public string Numero { get; set; }
        [Column("CEP")]
        [StringLength(9)]
        public string Cep { get; set; }
        [StringLength(255)]
        public string Cidade { get; set; }
        [StringLength(255)]
        public string Bairro { get; set; }
        [StringLength(2)]
        public string Estado { get; set; }

        [InverseProperty("IdEnderecoNavigation")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
