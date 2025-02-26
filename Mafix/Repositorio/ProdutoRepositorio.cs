using Mafix.Data;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        public readonly BancoContext _bancoContext;

        public ProdutoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ProdutoModel ListarPorId(int id)
        {
            return _bancoContext.Produtos.FirstOrDefault(x => x.Id == id);
        }

        public List<ProdutoModel> BuscarTodos()
        {
            return _bancoContext.Produtos.ToList();
        }

        public ProdutoModel Adicionar(ProdutoModel produto)
        {
            _bancoContext.Produtos.Add(produto);
            _bancoContext.SaveChanges();
            return produto;
        }

        public ProdutoModel Atualizar(ProdutoModel produto)
        {
            ProdutoModel produtoDb = ListarPorId(produto.Id);

            if (produtoDb == null) throw new Exception("Houve um erro na atualização do produto!");

            produtoDb.Nome = produto.Nome;
            produtoDb.Codigo = produto.Codigo;
            produtoDb.Modelo = produto.Modelo;
            produtoDb.Cor = produto.Cor;
            produtoDb.Quantidade =produto.Quantidade;
            
            _bancoContext.Produtos.Update(produtoDb);
            _bancoContext.SaveChanges();
            return produtoDb;
            
        }

        public bool Apagar(int id)
        {
            ProdutoModel produtoDb = ListarPorId(id);
            if (produtoDb == null) throw new Exception("Houve um erro na deleção do produto!");

            _bancoContext.Produtos.Remove(produtoDb);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
