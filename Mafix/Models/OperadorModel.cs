using System.ComponentModel.DataAnnotations;

namespace Mafix.Models
{
    public class OperadorModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Operador")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o cadastro do Operador")]
        public int? Cadastro { get; set; }
        [Required(ErrorMessage = "Digite a seção do Operador")]
        public int? Secao {  get; set; }

    }
}
