using Mafix.Models;

namespace Mafix.Repositorio
{
    public interface IRelatorioRepositorio
    {

        public List<ProducaoModel> BuscarProducaoMaquinaPorData(DateOnly dataInicio, DateOnly dataFim, int id);
        public List<ProducaoModel> BuscarProducaoOperadorPorData(DateOnly dataInicio, DateOnly dataFim);

        public ProducaoModel BuscarProducaoGeralMesOperador();
        public ProducaoModel BuscarProducaoGeralMesMaquina();
    }
}
