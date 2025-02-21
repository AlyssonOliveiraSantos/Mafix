using System.ComponentModel.DataAnnotations;

namespace Mafix.Models
{
    public class MaquinaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome da maquina!")]
        public string Nome { get; set; }

       [Required(ErrorMessage = "Digite a seção da maquina!")]
        public int? Secao { get; set; }

        public string? Descricao { get; set; }
    }
}
