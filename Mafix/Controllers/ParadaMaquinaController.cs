using Mafix.Filters;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Mafix.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ParadaMaquinaController : Controller
    {
        private readonly IParadaMaquinaRepositorio _paradaMaquinaRepositorio;
        public ParadaMaquinaController(IParadaMaquinaRepositorio paradaMaquinaRepositorio)
        {
            _paradaMaquinaRepositorio = paradaMaquinaRepositorio;
        }


        public IActionResult Index()
        {
            List<ParadaMaquinaModel> produtos = _paradaMaquinaRepositorio.BuscarTodos();
            return View(produtos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ParadaMaquinaModel paradaMaquina)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _paradaMaquinaRepositorio.Adicionar(paradaMaquina);
                    TempData["MensagemSucesso"] = "Hora parada de maquina cadastrada com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(paradaMaquina);
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos cadastrar sua hora parada de maquina, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Editar(int id)
        {

            ParadaMaquinaModel paradaMaquina = _paradaMaquinaRepositorio.ListarPorId(id);
            return View(paradaMaquina);
        }
        [HttpPost]
        public IActionResult Alterar(ParadaMaquinaModel paradaMaquina)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _paradaMaquinaRepositorio.Atualizar(paradaMaquina);
                    TempData["MensagemSucesso"] = "Hora parada de maquina editada com sucesso!";
                    return RedirectToAction("Index");
                }
                return View("Editar", paradaMaquina);
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos editar sua hora parada de maquina, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ParadaMaquinaModel produto = _paradaMaquinaRepositorio.ListarPorId(id);
            return View(produto);
        }

    
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _paradaMaquinaRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Hora parada de maquina apagada com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemError"] = "Ops! não conseguimos apagar sua hora parada de maquina";
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos apagar sua hora parada de maquina, tente novamente, {e.Message}";
                return RedirectToAction("Index");

            }

        }
    }
}
