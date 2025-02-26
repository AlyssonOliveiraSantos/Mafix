using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mafix.Models
{
    public class ProducaoModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Selecione o operador")]
        public int? OperadorId { get; set; }
        [Required(ErrorMessage = "Selecione a maquina")]
        public int? MaquinaId { get; set; }
        [Required(ErrorMessage = "Selecione o produto")]
        public int? ProdutoId { get; set; }

        [Required(ErrorMessage = "Selecione o motivo")]
        public int? ParadaMaquinaId {  get; set; }
        [Required(ErrorMessage = "Digite a quantidade produzida")]
        public int? QuantidadeProduzida { get; set; }
        [Required(ErrorMessage = "Digite a data de produção")]
        public DateOnly DataProducao { get; set; }
        [Required(ErrorMessage = "Digite a hora de inicio")]
        public TimeSpan HoraDeInicio { get; set; }
        [Required(ErrorMessage = "Digite a hora de fim")]
        public TimeSpan HoraDeFim { get; set; }
        public TimeSpan HoraParada { get; set; }
        public double Eficiencia { get; set; }

        public virtual OperadorModel Operador { get; set; }
        public virtual MaquinaModel Maquina { get; set; }
        public virtual ProdutoModel Produto { get; set; }
        public virtual ParadaMaquinaModel ParadaMaquina { get; set; }


        public void  ProducaoCalculada()
        {
            try
            {
                TimeSpan horaFim = HoraDeFim;
                TimeSpan horaInicio = HoraDeInicio;
                TimeSpan horaParada = HoraParada;
                TimeSpan tempoTotal;

                if (horaFim < horaInicio)
                {
                    horaFim = horaFim.Add(TimeSpan.FromDays(1));
                }
                if(ParadaMaquina.ContabilizaHoraParada == Enums.ContabilizaHoraParadaEnum.Contabiliza)
                {
                     tempoTotal = horaFim - horaInicio - horaParada;
                }
                else
                {
                     tempoTotal = horaFim - horaInicio;
                }



                double horasTrabalhadas = tempoTotal.TotalHours;
                if (horasTrabalhadas <= 0) return;
                if (Produto?.Quantidade <= 0) return;

                if (Produto?.Quantidade != null)
                {
                    Eficiencia = Math.Round(((Convert.ToDouble(QuantidadeProduzida) / Convert.ToDouble(horasTrabalhadas)) * 100) / Convert.ToDouble(Produto.Quantidade), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    throw new Exception("Ops, Não conseguimos realizar a operação");
                }
            }catch(Exception e)
            {
                throw new Exception($"Ops, Não conseguimos realizar a operação{e.Message}");
            }
        }
    }
}
