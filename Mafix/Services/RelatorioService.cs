using Mafix.Data;
using Mafix.DTOs;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Mafix.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Services
{
    public class RelatorioService : IRelatorioService
    {
        public readonly IProducaoRepositorio _producaoRepositorio;
        public readonly IMaquinaRepositorio _maquinaRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;

        public RelatorioService(IProducaoRepositorio producaoRepositorio,
                                IMaquinaRepositorio maquinaRepositorio,
                                IOperadorRepositorio operadorRepositorio)
        {
            _producaoRepositorio = producaoRepositorio;
            _maquinaRepositorio = maquinaRepositorio;
            _operadorRepositorio = operadorRepositorio;
        }

        public List<MaquinaModel> BuscarTodasMaquinas()
        {
           return  _maquinaRepositorio.BuscarTodas();
        }

        public List<OperadorModel> BuscarTodosOperadores()
        {
            return _operadorRepositorio.BuscarTodos();
        }


        public List<ProducaoDTO> BuscarProducaoGeralMesMaquina(DateOnly dataInicio, DateOnly dataFim)
        {
            List<ProducaoModel> producaoModel = _producaoRepositorio.BuscarProducaoGeralPorDataMaquinas(dataInicio, dataFim);

            List<ProducaoDTO> producaoDTO = producaoModel
                .GroupBy(x => x.Maquina.Nome)
                .Select(g => new ProducaoDTO
                {
                    MaquinaNome = g.Key,
                    QuantidadeProduzida = g.Sum(x => x.QuantidadeProduzida),
                    HoraParada = TimeSpan.FromTicks(g.Sum(x => x.HoraParada.Ticks)),
                    Eficiencia = g.Average(x => x.Eficiencia)
                })
                .ToList();

                foreach(var producao in producaoDTO)
                {
                    producao.HoraParadaString = producao.HoraParada.ToString(@"hh\:mm");
                }
            


            return producaoDTO;

        }

        public List<ProducaoDTO> BuscarProducaoMaquinaPorData(DateOnly dataInicio, DateOnly dataFim, int id)
        {

            List<ProducaoModel> producaoModel = _producaoRepositorio.BuscarProducaoPorDataMaquina(dataInicio, dataFim, id);



            List<ProducaoDTO> producaoDTO = producaoModel.Select(x => new ProducaoDTO
            {
                Id = x.Id,
                OperadorNome = x.Operador.Nome,
                MaquinaNome = x.Maquina.Nome,
                ProdutoNome = x.Produto.Nome,
                QuantidadeProduzida = x.QuantidadeProduzida,
                DataProducao = x.DataProducao,
                HoraDeInicio = x.HoraDeInicio,
                HoraDeFim = x.HoraDeFim,
                HoraParada = x.HoraParada,
                Eficiencia = x.Eficiencia,
                HoraDeInicioString = x.HoraDeInicio.ToString(@"hh\:mm"),
                HoraDeFimString = x.HoraDeFim.ToString(@"hh\:mm"),
                HoraParadaString = x.HoraParada.ToString(@"hh\:mm")
            }).ToList();



            return producaoDTO;
        }

        public List<ProducaoDTO> BuscarProducaoOperadorPorData(DateOnly dataInicio, DateOnly dataFim, int id)
        {
            List<ProducaoModel> producaoModel = _producaoRepositorio.BuscarProducaoPorDataOperador(dataInicio, dataFim, id);

            
            List<ProducaoDTO> producaoDTO =
                producaoModel.Select(x => new ProducaoDTO 
                {Id = x.Id, OperadorNome = x.Operador.Nome,
                    MaquinaNome = x.Maquina.Nome, 
                    ProdutoNome = x.Produto.Nome, 
                    QuantidadeProduzida = x.QuantidadeProduzida, 
                    DataProducao = x.DataProducao, 
                    HoraDeInicio = x.HoraDeInicio, 
                    HoraDeFim = x.HoraDeFim, 
                    HoraParada = x.HoraParada, 
                    Eficiencia = x.Eficiencia,
                    HoraDeInicioString = x.HoraDeInicio.ToString(@"hh\:mm"),
                    HoraDeFimString = x.HoraDeFim.ToString(@"hh\:mm"),
                    HoraParadaString = x.HoraParada.ToString(@"hh\:mm")
                }).ToList();

            return producaoDTO;
        }

    }
    
}
