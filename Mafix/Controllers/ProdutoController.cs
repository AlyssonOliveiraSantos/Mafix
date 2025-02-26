using Mafix.Filters;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Mafix.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }


        public IActionResult Index()
        {
            List<ProdutoModel> produtos = _produtoRepositorio.BuscarTodos();
            return View(produtos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _produtoRepositorio.Adicionar(produto);
                    TempData["MensagemSucesso"] = "Produto cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(produto);
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos cadastrar seu produto, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Editar(int id)
        {

            ProdutoModel produtoModel = _produtoRepositorio.ListarPorId(id);
            return View(produtoModel);
        }
        [HttpPost]
        public IActionResult Alterar(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _produtoRepositorio.Atualizar(produto);
                    TempData["MensagemSucesso"] = "Produto editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View("Editar", produto);
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos editar seu produto, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ProdutoModel produto = _produtoRepositorio.ListarPorId(id);
            return View(produto);
        }

    
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _produtoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Produto Apagado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemError"] = "Ops! não conseguimos apagar seu produto";
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos apagar seu produto, tente novamente, {e.Message}";
                return RedirectToAction("Index");

            }

        }
    }
}
