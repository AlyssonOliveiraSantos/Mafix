using Mafix.Models;
using Mafix.Repositorio;

namespace Mafix.Services
{
    public class ProducaoService : IProducaoService
    {
        private readonly IProducaoRepositorio _producaoRepositorio;
        
        public ProducaoService(IProducaoRepositorio producaoRepositorio)
        {
            _producaoRepositorio = producaoRepositorio;
        }

        public int CalculaProducaoGeralMaquina(int id)
        {
            List<ProducaoModel> producoes = _producaoRepositorio.ListarProducaoPorMaquina(id);
            int quantidadeProduzida = 0;
            int referencia = 0;


            foreach (var producao in producoes)
            {
                quantidadeProduzida += producao.QuantidadeProduzida;
                referencia += producao.QuantidadeProduzida;
            }
            return referencia > 0 ? (quantidadeProduzida * 100) / referencia : 0;
        }

        public int CalculaProducaoIndividualMaquina(ProducaoModel producao)
        {
            int quantidadeProduzida = producao.QuantidadeProduzida;
            int referencia = producao.QuantidadeProduzida;

            return referencia > 0 ? (quantidadeProduzida * 100) / referencia : 0;

        }
    }
}
