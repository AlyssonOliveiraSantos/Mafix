using Mafix.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mafix.Models
{
    public class ParadaMaquinaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o codigo")]
        public int? Codigo { get; set; }
        [Required(ErrorMessage = "Digite a descrição")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "Selecione uma opção")]
        public ContabilizaHoraParadaEnum? ContabilizaHoraParada { get; set; }


    }
}
