﻿using Mafix.Helper;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mafix.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                               ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            if(_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemError"] = "Login ou senha invalidos, tente novamente";
                    }

                    TempData["MensagemError"] = "Login ou senha invalidos, tente novamente";

                }
                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops! não foi possivel efetuar o login, tente novamente";
                Console.WriteLine(e.Message);
                return View("Index");
            }
        }
    }
}
