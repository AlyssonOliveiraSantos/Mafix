using Mafix.DTOs;
using Mafix.Models;

namespace Mafix.Services.Interfaces
{
    public interface IRelatorioService
    {
        public List<MaquinaModel> BuscarTodasMaquinas();
        public List<OperadorModel> BuscarTodosOperadores();
        public List<ProducaoDTO> BuscarProducaoMaquinaPorData(DateOnly dataInicio, DateOnly dataFim, int id);
        public List<ProducaoDTO> BuscarProducaoOperadorPorData(DateOnly dataInicio, DateOnly dataFim, int id);
        public List<ProducaoDTO> BuscarProducaoGeralMesMaquina(DateOnly dataInicio, DateOnly dataFim);

    }
}
