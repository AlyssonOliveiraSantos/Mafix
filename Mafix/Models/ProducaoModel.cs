using System.ComponentModel.DataAnnotations.Schema;

namespace Mafix.Models
{
    public class ProducaoModel
    {

        public int Id { get; set; }
        [ForeignKey("Operador")]
        public int OperadorId { get; set; }
        [ForeignKey("Maquina")]
        public int MaquinaId { get; set; }
        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public int QuantidadeProduzida { get; set; }
        public DateOnly DataProducao { get; set; }
        public TimeOnly HoraDeInicio { get; set; }
        public TimeOnly HoraDeFim { get; set; }
        public TimeOnly HoraParada{ get; set; }
        public double Eficiencia { get; set; }

        public virtual OperadorModel Operador { get; set; }
        public virtual MaquinaModel Maquina { get; set; }
        public virtual ProdutoModel Produto { get; set; }


        public void  ProducaoCalculada()
        {
            try
            {
                TimeSpan horaFim = HoraDeFim.ToTimeSpan();
                TimeSpan horaInicio = HoraDeInicio.ToTimeSpan();
                TimeSpan horaParada = HoraParada.ToTimeSpan();

                if (horaFim < horaInicio)
                {
                    horaFim = horaFim.Add(TimeSpan.FromDays(1));
                }

                TimeSpan tempoTotal = horaFim - horaInicio - horaParada;

                double horasTrabalhadas = tempoTotal.TotalHours;
                if (horasTrabalhadas <= 0) return;
                if (Produto?.Quantidade <= 0) return;

                if (Produto?.Quantidade != null)
                {
                    Eficiencia = Math.Round(((QuantidadeProduzida / horasTrabalhadas) * 100) / Produto.Quantidade, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    throw new Exception("Ops, Não conseguimos realizar a operação");
                }
            }catch(Exception e)
            {
                throw new Exception("Ops, Não conseguimos realizar a operação");
            }
        }
    }
}
