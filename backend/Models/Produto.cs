using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class Produto
    {
        public Produto()
        {
            RegistroProduto = new HashSet<RegistroProduto>();
            ReservaProduto = new HashSet<ReservaProduto>();
        }

        [Key]
        public int IdProduto { get; set; }
        [Required]
        [StringLength(255)]
        public string NomeProduto { get; set; }
        [Required]
        [StringLength(255)]
        public string ImgProduto { get; set; }
        public int? IdSobreProduto { get; set; }

        [ForeignKey(nameof(IdSobreProduto))]
        [InverseProperty(nameof(SobreProduto.Produto))]
        public virtual SobreProduto IdSobreProdutoNavigation { get; set; }
        [InverseProperty("IdProdutoNavigation")]
        public virtual ICollection<RegistroProduto> RegistroProduto { get; set; }
        [InverseProperty("IdProdutoNavigation")]
        public virtual ICollection<ReservaProduto> ReservaProduto { get; set; }
    }
}
