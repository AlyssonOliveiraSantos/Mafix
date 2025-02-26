using Mafix.DTOs;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Mafix.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mafix.Services
{
    public class ProducaoService : IProducaoService
    {
        public readonly IOperadorRepositorio _operadorRepositorio;
        public readonly IMaquinaRepositorio _maquinaRepositorio;
        public readonly IProdutoRepositorio _produtoRepositorio;
        public readonly IParadaMaquinaRepositorio _paradaMaquinaRepositorio;
        public readonly IProducaoRepositorio _producaoRepositorio;

        public ProducaoService(IOperadorRepositorio operadorRepositorio,
                               IMaquinaRepositorio maquinaRepositorio, 
                               IProdutoRepositorio produtoRepositorio, 
                               IProducaoRepositorio producaoRepositorio,
                               IParadaMaquinaRepositorio paradaMaquinaRepositorio)
        {
            _operadorRepositorio = operadorRepositorio;
            _maquinaRepositorio = maquinaRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _producaoRepositorio = producaoRepositorio;
            _paradaMaquinaRepositorio = paradaMaquinaRepositorio;
        }

        public List<ProducaoDTO> BuscarTodasProducoesDTO()
        {
            List<ProducaoModel> producoes = _producaoRepositorio.BuscarTodos();
            return producoes.Select(x => new ProducaoDTO
            {
                Id = x.Id,
                OperadorNome = x.Operador?.Nome ?? "Vazio",
                MaquinaNome = x.Maquina?.Nome ?? "Vazio",
                ProdutoNome = x.Produto?.Nome ?? "Vazio",
                QuantidadeProduzida = x.QuantidadeProduzida,
                ParadaMaquinaCodigo = x.ParadaMaquina?.Codigo ?? 0,
                DataProducao = x.DataProducao,
                HoraDeInicio = x.HoraDeInicio,
                HoraDeFim = x.HoraDeFim,
                HoraParada = x.HoraParada,
                Eficiencia = x.Eficiencia,
                HoraDeInicioString = x.HoraDeInicio.ToString(@"hh\:mm"),
                HoraDeFimString = x.HoraDeFim.ToString(@"hh\:mm"),
                HoraParadaString = x.HoraParada.ToString(@"hh\:mm")
            }).ToList();
        }

        public ProducaoDTO BuscarTodasProducoesDTOPorId(int id)
        {
            ProducaoModel producao = _producaoRepositorio.ListarPorId(id);

            if (producao == null)
            {
                return null;
            }

            return new ProducaoDTO
            {
                Id = producao.Id,
                OperadorNome = producao.Operador?.Nome ?? "Vazio", 
                MaquinaNome = producao.Maquina?.Nome ?? "Vazio", 
                ProdutoNome = producao.Produto?.Nome ?? "Vazio", 
                QuantidadeProduzida = producao.QuantidadeProduzida,
                ParadaMaquinaCodigo = producao.ParadaMaquina?.Codigo ?? 0, 
                DataProducao = producao.DataProducao,

                
                HoraDeInicio = producao.HoraDeInicio,
                HoraDeFim = producao.HoraDeFim,
                HoraParada = producao.HoraParada,

                
                HoraDeInicioString = producao.HoraDeInicio.ToString(@"hh\:mm"),
                HoraDeFimString = producao.HoraDeFim.ToString(@"hh\:mm"),
                HoraParadaString = producao.HoraParada.ToString(@"hh\:mm")
            };
        }


        public void AdicionarProducaoDTO(ProducaoDTO producaoDTO)
        {
            ProducaoModel producao = new ProducaoModel
            {
                Id = producaoDTO.Id,
                OperadorId = _producaoRepositorio.BuscarOperadorPorId(producaoDTO.OperadorId)?.Id,
                MaquinaId = _producaoRepositorio.BuscarMaquinaPorId(producaoDTO.MaquinaId)?.Id,
                ProdutoId = _producaoRepositorio.BuscarProdutoPorId(producaoDTO.ProdutoId)?.Id,
                ParadaMaquinaId = _producaoRepositorio.BuscarParadaMaquinaPorId(producaoDTO.ParadaMaquinaId).Id,
                QuantidadeProduzida = producaoDTO.QuantidadeProduzida,
                DataProducao = producaoDTO.DataProducao,
                HoraDeInicio = producaoDTO.HoraDeInicio,
                HoraDeFim = producaoDTO.HoraDeFim,
                HoraParada = producaoDTO.HoraParada,
                Eficiencia = producaoDTO.Eficiencia
            };
            _producaoRepositorio.Adicionar(producao);
        }

        public void AtualizarProducaoDTO(ProducaoDTO producaoDTO)
        {
            ProducaoModel producao = new ProducaoModel
            {
                Id = producaoDTO.Id,
                OperadorId = _producaoRepositorio.BuscarOperadorPorId(producaoDTO.OperadorId)?.Id,
                MaquinaId = _producaoRepositorio.BuscarMaquinaPorId(producaoDTO.MaquinaId)?.Id,
                ProdutoId = _producaoRepositorio.BuscarProdutoPorId(producaoDTO.ProdutoId)?.Id,
                ParadaMaquinaId = _producaoRepositorio.BuscarParadaMaquinaPorId(producaoDTO.ParadaMaquinaId).Id,
                QuantidadeProduzida = producaoDTO.QuantidadeProduzida,
                DataProducao = producaoDTO.DataProducao,
                HoraDeInicio = producaoDTO.HoraDeInicio,
                HoraDeFim = producaoDTO.HoraDeFim,
                HoraParada = producaoDTO.HoraParada,
                Eficiencia = producaoDTO.Eficiencia
            };

            _producaoRepositorio.Atualizar(producao);
        }

        public bool ApagarProducaoDTOPorId(int id)
        {
            return _producaoRepositorio.Apagar(id);
        }

        public List<MaquinaModel> BuscarTodasMaquinas()
        {
           return _maquinaRepositorio.BuscarTodas();
        }

        public List<OperadorModel> BuscarTodosOperadores()
        {
           return _operadorRepositorio.BuscarTodos();
        }

        public List<ProdutoModel> BuscarTodosProdutos()
        {
            return _produtoRepositorio.BuscarTodos();
        }

        public List<ParadaMaquinaModel> BuscarTodasParadasMaquinas()
        {
            return _paradaMaquinaRepositorio.BuscarTodos();
        }


    }
}
