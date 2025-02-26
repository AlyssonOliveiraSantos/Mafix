using Mafix.Data;
using Mafix.DTOs;
using Mafix.Enums;
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



        public List<ProducaoDTO> BuscarProducaoGeralMesMaquina()
        {
            DateOnly firstDayOfMonth = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateOnly lastDayOfMonth = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            var producoes = _bancoContext.Producao
                .Where(x => x.DataProducao >= firstDayOfMonth && x.DataProducao <= lastDayOfMonth)
                .Select(x => new
                {
                    MaquinaId = x.Maquina.Id,
                    MaquinaNome = x.Maquina.Nome,
                    QuantidadeProduzida = x.QuantidadeProduzida,
                    HoraParadaTicks = x.HoraParada.Ticks, 
                    Eficiencia = x.Eficiencia
                })
                .ToList() 
                .GroupBy(x => new { x.MaquinaId, x.MaquinaNome })
                .Select(g => new ProducaoDTO
                {
                    MaquinaNome = g.Key.MaquinaNome,
                    QuantidadeProduzida = g.Sum(x => x.QuantidadeProduzida),
                    HoraParada = TimeSpan.FromTicks(g.Sum(x => x.HoraParadaTicks)), 
                    Eficiencia = g.Average(x => x.Eficiencia),
                })
                .ToList();

            foreach(var producao in producoes)
            {
                producao.HoraParadaString = producao.HoraParada.ToString(@"hh\:mm");
            }

            producoes.Reverse();


            return producoes;

        }

        public List<ProducaoDTO> BuscarProducaoGeralMesOperador()
        {
            throw new NotImplementedException();
        }

        public List<ProducaoDTO> BuscarProducaoMaquinaPorData(DateOnly dataInicio, DateOnly dataFim, int id)
        {

            List<ProducaoDTO> producaoDTO = _bancoContext.Producao.Where(x => x.DataProducao >= dataInicio && x.DataProducao <= dataFim && x.MaquinaId == id)
                .Include(x => x.Operador)
                .Include(x => x.Maquina)
                .OrderBy(x => x.DataProducao)
                .Select(x => new ProducaoDTO 
                {Id = x.Id, OperadorNome = x.Operador.Nome,
                    MaquinaNome = x.Maquina.Nome, 
                    ProdutoNome = x.Produto.Nome, 
                    QuantidadeProduzida = x.QuantidadeProduzida, 
                    DataProducao = x.DataProducao, 
                    HoraDeInicio = x.HoraDeInicio,
                    HoraDeFim = x.HoraDeFim, 
                    HoraParada = x.HoraParada, 
                    Eficiencia = x.Eficiencia, 
                    HoraDeInicioString  = x.HoraDeInicio.ToString(@"hh\:mm"),
                    HoraDeFimString = x.HoraDeFim.ToString(@"hh\:mm"),
                    HoraParadaString = x.HoraParada.ToString(@"hh\:mm")
                }).ToList();



            return producaoDTO;
        }

        public List<ProducaoDTO> BuscarProducaoOperadorPorData(DateOnly dataInicio, DateOnly dataFim, int id)
        {
            List<ProducaoDTO> producaoDTO = _bancoContext.Producao.Where(x => x.DataProducao >= dataInicio && x.DataProducao <= dataFim && x.OperadorId == id)
                .Include(x => x.Operador)
                .Include(x => x.Maquina)
                .OrderBy(x => x.DataProducao)
                .Select(x => new ProducaoDTO 
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
