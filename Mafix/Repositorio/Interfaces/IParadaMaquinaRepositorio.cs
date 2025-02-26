using Mafix.Models;

namespace Mafix.Repositorio.Interfaces
{
    public interface IParadaMaquinaRepositorio
    {
        ParadaMaquinaModel ListarPorId(int id);
        List<ParadaMaquinaModel> BuscarTodos();
        ParadaMaquinaModel Adicionar(ParadaMaquinaModel produto);
        ParadaMaquinaModel Atualizar(ParadaMaquinaModel produto);
        bool Apagar(int id);
    }
}
