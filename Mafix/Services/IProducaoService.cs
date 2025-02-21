using Mafix.Models;

namespace Mafix.Services
{
    public interface IProducaoService
    {
        public int CalculaProducaoGeralMaquina(int id);
        public int CalculaProducaoIndividualMaquina(ProducaoModel producao);

    }
}
