using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class RegistroProduto
    {
        [Key]
        public int IdRegistro { get; set; }
        public int? IdProduto { get; set; }
        public int? IdColaborador { get; set; }

        [ForeignKey(nameof(IdColaborador))]
        [InverseProperty(nameof(Colaborador.RegistroProduto))]
        public virtual Colaborador IdColaboradorNavigation { get; set; }
        [ForeignKey(nameof(IdProduto))]
        [InverseProperty(nameof(Produto.RegistroProduto))]
        public virtual Produto IdProdutoNavigation { get; set; }
    }
}
