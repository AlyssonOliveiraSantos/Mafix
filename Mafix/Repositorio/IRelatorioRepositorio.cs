using Mafix.DTOs;
using Mafix.Models;

namespace Mafix.Repositorio
{
    public interface IRelatorioRepositorio
    {

        public List<ProducaoDTO> BuscarProducaoMaquinaPorData(DateOnly dataInicio, DateOnly dataFim, int id);
        public List<ProducaoDTO> BuscarProducaoOperadorPorData(DateOnly dataInicio, DateOnly dataFim, int id);

        public List<ProducaoDTO> BuscarProducaoGeralMesOperador();
        public List<ProducaoDTO> BuscarProducaoGeralMesMaquina();
    }
}
