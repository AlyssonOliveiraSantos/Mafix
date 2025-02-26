using Mafix.Filters;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Mafix.Controllers
{
    [PaginaParaUsuarioLogado]
    public class OperadorController : Controller
    {
        private readonly IOperadorRepositorio _operadorRepositorio;
        public OperadorController(IOperadorRepositorio operadorRepositorio)
        {
            _operadorRepositorio = operadorRepositorio;
        }
        public IActionResult Index()
        {
            List<OperadorModel> operadores = _operadorRepositorio.BuscarTodos();
            return View(operadores);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(OperadorModel operador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _operadorRepositorio.Adicionar(operador);
                    TempData["MensagemSucesso"] = "Operador cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(operador);
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos cadastrar seu operador, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Editar(int id)
        {   

            OperadorModel operadorModel = _operadorRepositorio.ListarPorId(id);
            return View(operadorModel);
        }
        [HttpPost]
        public IActionResult Alterar(OperadorModel operador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _operadorRepositorio.Atualizar(operador);
                    TempData["MensagemSucesso"] = "Operador editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View("Editar", operador);
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos editar seu operador, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            OperadorModel operador = _operadorRepositorio.ListarPorId(id);
            return View(operador);
        }

    
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _operadorRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Operador Apagado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemError"] = "Ops! não conseguimos apagar seu operador";
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos apagar seu operador, tente novamente, {e.Message}";
                return RedirectToAction("Index");

            }

        }
    }
}
