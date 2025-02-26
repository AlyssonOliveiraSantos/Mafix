using System.ComponentModel.DataAnnotations;

namespace Mafix.DTOs
{
    public class ProducaoDTO
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Selecione o operador")]
        public string OperadorNome { get; set; }
        public int OperadorId { get; set; }
        [Required(ErrorMessage = "Selecione a maquina")]
        public string MaquinaNome { get; set; }
        public int MaquinaId { get; set; }
        [Required(ErrorMessage = "Selecione o produto")]
        public string ProdutoNome { get; set; }
        public int ProdutoId { get; set; }
        public int ParadaMaquinaId { get; set; }
        public int ParadaMaquinaCodigo { get; set; }

        [Required(ErrorMessage = "Digite a quantidade produzida")]
        public int? QuantidadeProduzida { get; set; }
        [Required(ErrorMessage = "Digite a data de produção")]
        public DateOnly DataProducao { get; set; }
        [Required(ErrorMessage = "Digite a hora de inicio")]
        public TimeSpan HoraDeInicio { get; set; }
        [Required(ErrorMessage = "Digite a hora de fim")]
        public string HoraDeInicioString { get; set; }
        public TimeSpan HoraDeFim { get; set; }
        public string HoraDeFimString { get; set; }
        public TimeSpan HoraParada { get; set; }
        public string HoraParadaString { get; set; }
        public double Eficiencia { get; set; }
        public double MediaEficiencia { get; set; }

    }
}
