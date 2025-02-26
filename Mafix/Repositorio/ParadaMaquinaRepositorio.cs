using Mafix.Data;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Repositorio
{
    public class ParadaMaquinaRepositorio : IParadaMaquinaRepositorio
    {
        public readonly BancoContext _bancoContext;

        public ParadaMaquinaRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ParadaMaquinaModel ListarPorId(int id)
        {
            return _bancoContext.ParadaMaquina.FirstOrDefault(x => x.Id == id);
        }

        public List<ParadaMaquinaModel> BuscarTodos()
        {
            return _bancoContext.ParadaMaquina.ToList();
        }

        public ParadaMaquinaModel Adicionar(ParadaMaquinaModel produto)
        {
            _bancoContext.ParadaMaquina.Add(produto);
            _bancoContext.SaveChanges();
            return produto;
        }

        public ParadaMaquinaModel Atualizar(ParadaMaquinaModel paradaMaquina)
        {
            ParadaMaquinaModel paradaMaquinaDb = ListarPorId(paradaMaquina.Id);

            if (paradaMaquinaDb == null) throw new Exception("Houve um erro na atualização do produto!");

            paradaMaquinaDb.Codigo = paradaMaquina.Codigo;
            paradaMaquinaDb.ContabilizaHoraParada = paradaMaquina.ContabilizaHoraParada;
            paradaMaquinaDb.Descricao = paradaMaquina.Descricao;
            
            _bancoContext.ParadaMaquina.Update(paradaMaquinaDb);
            _bancoContext.SaveChanges();
            return paradaMaquinaDb;
            
        }

        public bool Apagar(int id)
        {
            ParadaMaquinaModel produtoDb = ListarPorId(id);
            if (produtoDb == null) throw new Exception("Houve um erro na deleção do produto!");

            _bancoContext.ParadaMaquina.Remove(produtoDb);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
