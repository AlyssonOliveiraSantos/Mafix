using Mafix.Data;
using Mafix.Models;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Repositorio
{
    public class RelatorioRepositorio : IRelatorioRepositorio
    {
        public BancoContext _bancoContext;

        public RelatorioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }



        public ProducaoModel BuscarProducaoGeralMesMaquina()
        {
            throw new NotImplementedException();
        }

        public ProducaoModel BuscarProducaoGeralMesOperador()
        {
            throw new NotImplementedException();
        }

        public List<ProducaoModel> BuscarProducaoMaquinaPorData(DateOnly dataInicio, DateOnly dataFim, int id)
        {

            List<ProducaoModel> producao = _bancoContext.Producao.Where(x => x.DataProducao > dataInicio && x.DataProducao < dataFim && x.MaquinaId == id).Include(x => x.Maquina).Include(x => x.Operador).ToList(); ;
            
            return producao;
        }

        public List<ProducaoModel> BuscarProducaoOperadorPorData(DateOnly dataInicio, DateOnly dataFim)
        {
            throw new NotImplementedException();
        }
    }
    
}
