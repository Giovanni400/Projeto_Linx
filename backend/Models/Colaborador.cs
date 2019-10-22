using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class Colaborador
    {
        public Colaborador()
        {
            RegistroProduto = new HashSet<RegistroProduto>();
        }

        [Key]
        public int IdColaborador { get; set; }
        [StringLength(255)]
        public string ImgPerfil { get; set; }
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [StringLength(20)]
        public string DocumentoColab { get; set; }
        public bool? FazEntrega { get; set; }
        [Column(TypeName = "text")]
        public string SobreColab { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Colaborador))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
        [InverseProperty("IdColaboradorNavigation")]
        public virtual ICollection<RegistroProduto> RegistroProduto { get; set; }
    }
}
