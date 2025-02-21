using System.ComponentModel.DataAnnotations;

namespace Mafix.Models
{
    public class ProdutoModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do produto")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o codigo do produto")]
        public int? Codigo { get; set; }
        [Required(ErrorMessage = "Digite o modelo do produto")]
        public string Modelo {  get; set; }
        [Required(ErrorMessage = "Digite a cor do produto")]
        public string Cor {  get; set; }
        [Required(ErrorMessage = "Digite a quantidade que equivale a 100% em uma hora")]
        public int Quantidade { get; set; }


    }
}
