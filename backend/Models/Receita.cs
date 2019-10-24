using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class Receita
    {
        [Key]
        public int IdReceita { get; set; }
        [Required]
        [StringLength(255)]
        public string NomeReceita { get; set; }
        [Required]
        [StringLength(255)]
        public string ImgReceita { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string DescricaoIngrediente { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string DescricaoPreparo { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Receita))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
