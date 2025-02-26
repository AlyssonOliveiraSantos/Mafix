using Mafix.Data;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Repositorio
{
    public class OperadorRepositorio : IOperadorRepositorio
    {
        public readonly BancoContext _bancoContext;

        public OperadorRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public OperadorModel ListarPorId(int id)
        {
            return _bancoContext.Operadores.FirstOrDefault(x => x.Id == id);
            
        }

        public List<OperadorModel> BuscarTodos()
        {
            return _bancoContext.Operadores.ToList();
        }

        public OperadorModel Adicionar(OperadorModel operador)
        {
            _bancoContext.Operadores.Add(operador);
            _bancoContext.SaveChanges();
            return operador;
        }

        public OperadorModel Atualizar(OperadorModel operador)
        {
            OperadorModel operadorDb = ListarPorId(operador.Id);

            if (operadorDb == null) throw new Exception("Houve um erro na atualização do operador!");

            operadorDb.Nome = operador.Nome;
            operadorDb.Cadastro = operador.Cadastro;
            operadorDb.Secao = operador.Secao;

            _bancoContext.Operadores.Update(operadorDb);
            _bancoContext.SaveChanges();
            return operadorDb;
            
        }

        public bool Apagar(int id)
        {
            OperadorModel operadorDb = ListarPorId(id);
            if (operadorDb == null) throw new Exception("Houve um erro na deleção do operador!");

            _bancoContext.Operadores.Remove(operadorDb);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
