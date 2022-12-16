using System;
using System.ComponentModel.DataAnnotations;
using Teste_Upd8.Models.Enums;

namespace Teste_Upd8.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Data de Nascimento requerido")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public string Estado { get; set; } 
        public string Cidade { get; set; } 
    }
}
