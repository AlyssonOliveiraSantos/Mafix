using Mafix.DTOs;
using Mafix.Models;

namespace Mafix.Repositorio.Interfaces
{
    public interface IProducaoRepositorio
    {
        ProducaoModel ListarPorId(int id);
        List<ProducaoModel> ListarProducaoPorMaquina(int id);
        List<ProducaoModel> BuscarTodos();
        ProducaoModel Adicionar(ProducaoModel operador);
        ProducaoModel Atualizar(ProducaoModel operador);
        bool Apagar(int id);
        List<ProducaoModel> BuscarProducaoPorDataMaquina(DateOnly dataInicio, DateOnly dataFim, int id);
        List<ProducaoModel> BuscarProducaoPorDataOperador(DateOnly dataInicio, DateOnly dataFim, int id);
        List<ProducaoModel> BuscarProducaoGeralPorDataMaquinas(DateOnly dataInicio, DateOnly dataFim);

        OperadorModel BuscarOperadorPorId(int? id);
        MaquinaModel BuscarMaquinaPorId(int? id);
        ProdutoModel BuscarProdutoPorId(int? id);
        ParadaMaquinaModel BuscarParadaMaquinaPorId(int? id);


    }
}
