using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class SobreProduto
    {
        public SobreProduto()
        {
            Produto = new HashSet<Produto>();
        }

        [Key]
        public int IdSobreProduto { get; set; }
        [Required]
        [StringLength(255)]
        public string DescricaoProduto { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Disponibilidade { get; set; }
        public bool Organico { get; set; }
        [Required]
        [StringLength(20)]
        public string Preco { get; set; }
        [Required]
        [StringLength(20)]
        public string Validade { get; set; }

        [InverseProperty("IdSobreProdutoNavigation")]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
