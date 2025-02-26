using Mafix.DTOs;
using Mafix.Models;

namespace Mafix.Services.Interfaces
{
    public interface IProducaoService
    {
        public List<ProducaoDTO> BuscarTodasProducoesDTO();
        public ProducaoDTO BuscarTodasProducoesDTOPorId(int id);
        public void AdicionarProducaoDTO(ProducaoDTO producaoDTO);
        public void AtualizarProducaoDTO(ProducaoDTO producaoDTO);
        public bool ApagarProducaoDTOPorId(int id);



        public List<MaquinaModel> BuscarTodasMaquinas();
        public List<OperadorModel> BuscarTodosOperadores();
        public List<ProdutoModel> BuscarTodosProdutos();
        public List<ParadaMaquinaModel> BuscarTodasParadasMaquinas();

    }
}
