using Mafix.Filters;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Mafix.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }


        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos cadastrar seu usuario, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Editar(int id)
        {

            UsuarioModel usuarioModel = _usuarioRepositorio.ListarPorId(id);
            return View(usuarioModel);
        }
        [HttpPost]
        public IActionResult Alterar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuario editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View("Editar", usuario);
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos editar seu usuario, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

    
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuario Apagado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemError"] = "Ops! não conseguimos apagar seu usuario";
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos apagar seu usuario, tente novamente, {e.Message}";
                return RedirectToAction("Index");

            }

        }
    }
}
