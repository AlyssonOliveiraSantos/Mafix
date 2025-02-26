using Mafix.Models;

namespace Mafix.Repositorio
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
