using Mafix.Data;
using Mafix.Models;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Repositorio
{
    public class ProducaoRepositorio : IProducaoRepositorio
    {
        public readonly BancoContext _bancoContext;

        public ProducaoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }



        public ProducaoModel ListarPorId(int id)
        {
            return _bancoContext.Producao.Include(x => x.Produto).Include(x => x.ParadaMaquina).FirstOrDefault(x => x.Id == id);

        }

        public List<ProducaoModel> ListarProducaoPorMaquina(int id)
        {
            return _bancoContext.Producao.Where(x => x.MaquinaId == id).ToList();

        }

        public List<ProducaoModel> BuscarTodos()
        {
            return _bancoContext.Producao
                .Include(x => x.Operador).Include(x => x.Produto).Include(x => x.Maquina).Include(x => x.ParadaMaquina)
                .ToList();
        }


        public ProducaoModel Adicionar(ProducaoModel producao)
        {
            producao.Produto = _bancoContext.Produtos.FirstOrDefault(x => x.Id == producao.ProdutoId);
            producao.ParadaMaquina = _bancoContext.ParadaMaquina.FirstOrDefault(x => x.Id == producao.ParadaMaquinaId);

            producao.ProducaoCalculada();
            _bancoContext.Producao.Add(producao);
            _bancoContext.SaveChanges();
            return producao;
        }

        public ProducaoModel Atualizar(ProducaoModel producao)
        {
            ProducaoModel producaoDb = ListarPorId(producao.Id);
            if (producaoDb?.Produto == null)
            {
                throw new Exception("Produto não encontrado para a produção.");
            }

            if (producaoDb == null) throw new Exception("Houve um erro na atualização da producao!");

            producaoDb.OperadorId = producao.OperadorId;
            producaoDb.MaquinaId = producao.MaquinaId;
            producaoDb.ProdutoId = producao.ProdutoId;
            producaoDb.ParadaMaquinaId = producao.ParadaMaquinaId;
            producaoDb.QuantidadeProduzida = producao.QuantidadeProduzida;
            producaoDb.DataProducao = producao.DataProducao;
            producaoDb.HoraDeInicio = producao.HoraDeInicio;
            producaoDb.HoraDeFim = producao.HoraDeFim;
            producaoDb.HoraParada = producao.HoraParada;
            producaoDb.Produto = _bancoContext.Produtos.FirstOrDefault(x => x.Id == producao.ProdutoId);
            producaoDb.ParadaMaquina = _bancoContext.ParadaMaquina.FirstOrDefault(x => x.Id == producao.ParadaMaquinaId);
            producaoDb.ProducaoCalculada();
           
            _bancoContext.Producao.Update(producaoDb);
            _bancoContext.SaveChanges();
            return producaoDb;
            
        }

        public bool Apagar(int id)
        {
            ProducaoModel producaoDb = ListarPorId(id);
            if (producaoDb == null) throw new Exception("Houve um erro na deleção da producao!");

            _bancoContext.Producao.Remove(producaoDb);
            _bancoContext.SaveChanges();
            return true;
        }


    }
}
