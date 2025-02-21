using Mafix.Data;
using Mafix.Models;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Repositorio
{
    public class MaquinaRepositorio : IMaquinaRepositorio
    {
        public readonly BancoContext _bancoContext;

        public MaquinaRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public MaquinaModel ListarPorId(int id)
        {
            return _bancoContext.Maquinas.FirstOrDefault(x => x.Id == id);
        }

        public List<MaquinaModel> BuscarTodas()
        {
            return _bancoContext.Maquinas.ToList();
        }

        public MaquinaModel Adicionar(MaquinaModel maquina)
        {
            _bancoContext.Maquinas.Add(maquina);
            _bancoContext.SaveChanges();
            return maquina;
        }

        public MaquinaModel Atualizar(MaquinaModel maquina)
        {
            MaquinaModel maquinaDb = ListarPorId(maquina.Id);

            if (maquinaDb == null) throw new Exception("Houve um erro na atualização da maquina!");

            maquinaDb.Nome = maquina.Nome;
            maquinaDb.Secao = maquina.Secao;
            maquinaDb.Descricao = maquina.Descricao;

            _bancoContext.Maquinas.Update(maquinaDb);
            _bancoContext.SaveChanges();
            return maquinaDb;
            
        }

        public bool Apagar(int id)
        {
            MaquinaModel maquinaDb = ListarPorId(id);
            if (maquinaDb == null) throw new Exception("Houve um erro na deleção da maquina!");

            _bancoContext.Maquinas.Remove(maquinaDb);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
