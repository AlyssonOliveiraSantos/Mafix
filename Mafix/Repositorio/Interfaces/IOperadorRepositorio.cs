using Mafix.Models;

namespace Mafix.Repositorio.Interfaces
{
    public interface IOperadorRepositorio
    {
        OperadorModel ListarPorId(int id);
        List<OperadorModel> BuscarTodos();
        OperadorModel Adicionar(OperadorModel operador);
        OperadorModel Atualizar(OperadorModel operador);
        bool Apagar(int id);
    }
}
