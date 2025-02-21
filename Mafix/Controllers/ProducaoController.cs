using Mafix.Filters;
using Mafix.Models;
using Mafix.Repositorio;
using Mafix.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ProducaoController : Controller
    {
        private readonly IProducaoRepositorio _producaoRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;
        private readonly IMaquinaRepositorio _maquinaRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IProducaoService _producaoService;
        
        public ProducaoController(IProducaoRepositorio producaoRepositorio,
                                  IOperadorRepositorio operadorRepositorio,
                                  IMaquinaRepositorio maquinaRepositorio,
                                  IProdutoRepositorio produtoRepositorio,
                                  IProducaoService producaoService)
        {
            _producaoRepositorio = producaoRepositorio;
            _operadorRepositorio = operadorRepositorio;
            _maquinaRepositorio = maquinaRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _producaoService = producaoService;
            
        }


        public IActionResult Index()
        {
            List<ProducaoModel> producoes = _producaoRepositorio.BuscarTodos();
            return View(producoes);

        }

        public IActionResult Criar()
        {
            List<OperadorModel> operadores = _operadorRepositorio.BuscarTodos();
            List<MaquinaModel> maquinas = _maquinaRepositorio.BuscarTodas();
            List<ProdutoModel> produtos = _produtoRepositorio.BuscarTodos();
            ViewBag.Operadores = new SelectList(operadores, "Id", "Nome");
            ViewBag.Maquinas = new SelectList(maquinas, "Id", "Nome");
            ViewBag.Produtos = new SelectList(produtos, "Id", "Nome");
            return View();
        }


        [HttpPost]
        public IActionResult Criar(ProducaoModel producao)
        {
            try
            {
                //  if (ModelState.IsValid)
                // {
                    _producaoRepositorio.Adicionar(producao);
                    TempData["MensagemSucesso"] = "Produção cadastrada com sucesso!";
                    return RedirectToAction("Index");
                //}

              //  return View(producao);
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos cadastrar sua produção, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Editar(int id)
        {
            List<OperadorModel> operadores = _operadorRepositorio.BuscarTodos();
            List<MaquinaModel> maquinas = _maquinaRepositorio.BuscarTodas();
            List<ProdutoModel> produtos = _produtoRepositorio.BuscarTodos();
            ViewBag.Operadores = new SelectList(operadores, "Id", "Nome");
            ViewBag.Maquinas = new SelectList(maquinas, "Id", "Nome");
            ViewBag.Produtos = new SelectList(produtos, "Id", "Nome");

            ProducaoModel producaoModel = _producaoRepositorio.ListarPorId(id);
            return View(producaoModel);
        }
        [HttpPost]
        public IActionResult Alterar(ProducaoModel producao)
        {
            try
            {
            //    if (ModelState.IsValid)
             //   {
                    _producaoRepositorio.Atualizar(producao);
                    TempData["MensagemSucesso"] = "Produção editada com sucesso!";
                    return RedirectToAction("Index");
               // }
               // return View("Editar", producao);
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos editar sua produção, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ProducaoModel producao = _producaoRepositorio.ListarPorId(id);
            return View(producao);
        }

    
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _producaoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Produção apagada com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemError"] = "Ops! não conseguimos apagar sua produção";
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos apagar sua produção, tente novamente, {e.Message}";
                return RedirectToAction("Index");

            }

        }
    }
}
