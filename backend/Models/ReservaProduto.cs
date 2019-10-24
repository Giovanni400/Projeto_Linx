using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class ReservaProduto
    {
        [Key]
        public int IdReserva { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Quantidade { get; set; }
        public int? IdProduto { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdProduto))]
        [InverseProperty(nameof(Produto.ReservaProduto))]
        public virtual Produto IdProdutoNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.ReservaProduto))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
