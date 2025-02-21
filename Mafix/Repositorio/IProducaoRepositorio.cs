using Mafix.Models;

namespace Mafix.Repositorio
{
    public interface IProducaoRepositorio
    {
        ProducaoModel ListarPorId(int id);
        List<ProducaoModel> ListarProducaoPorMaquina(int id);
        List<ProducaoModel> BuscarTodos();
        ProducaoModel Adicionar(ProducaoModel operador);
        ProducaoModel Atualizar(ProducaoModel operador);
        bool Apagar(int id);
    }
}
