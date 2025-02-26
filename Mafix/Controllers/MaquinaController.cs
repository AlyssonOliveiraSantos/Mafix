using Mafix.Filters;
using Mafix.Models;
using Mafix.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Mafix.Controllers
{
    [PaginaParaUsuarioLogado]
    public class MaquinaController : Controller
    {
        private readonly IMaquinaRepositorio _maquinaRepositorio;
        public MaquinaController(IMaquinaRepositorio maquinaRepositorio)
        {
            _maquinaRepositorio = maquinaRepositorio;
        }
        public IActionResult Index()
        {
            List<MaquinaModel> maquinas = _maquinaRepositorio.BuscarTodas();
            return View(maquinas);
        } 

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(MaquinaModel maquina)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _maquinaRepositorio.Adicionar(maquina);
                    TempData["MensagemSucesso"] = "Maquina cadastrada com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(maquina);
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos cadastrar sua maquina, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Editar(int id)
        {   

            MaquinaModel maquinaModel = _maquinaRepositorio.ListarPorId(id);
            return View(maquinaModel);
        }
        [HttpPost]
        public IActionResult Alterar(MaquinaModel maquina)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _maquinaRepositorio.Atualizar(maquina);
                    TempData["MensagemSucesso"] = "Maquina editada com sucesso!";
                    return RedirectToAction("Index");
                }
                return View("Editar", maquina);
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos editar sua maquina, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            MaquinaModel maquina = _maquinaRepositorio.ListarPorId(id);
            return View(maquina);
        }

    
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _maquinaRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Maquina apagada com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemError"] = "Ops! não conseguimos apagar sua maquina";
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos apagar sua maquina, tente novamente, {e.Message}";
                return RedirectToAction("Index");

            }

        }
    }
}
