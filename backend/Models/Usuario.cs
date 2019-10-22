using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Colaborador = new HashSet<Colaborador>();
            Receita = new HashSet<Receita>();
            ReservaProduto = new HashSet<ReservaProduto>();
        }

        [Key]
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(255)]
        public string NomeUsuario { get; set; }
        [Required]
        [StringLength(255)]
        public string EmailUsuario { get; set; }
        [Required]
        [StringLength(8)]
        public string SenhaUsuario { get; set; }
        public bool? ReceberNotif { get; set; }
        [StringLength(20)]
        public string TipoUsuario { get; set; }
        [StringLength(15)]
        public string Telefone1 { get; set; }
        [StringLength(15)]
        public string Telefone2 { get; set; }
        public int? IdEndereco { get; set; }

        [ForeignKey(nameof(IdEndereco))]
        [InverseProperty(nameof(Endereco.Usuario))]
        public virtual Endereco IdEnderecoNavigation { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Colaborador> Colaborador { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Receita> Receita { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<ReservaProduto> ReservaProduto { get; set; }
    }
}
