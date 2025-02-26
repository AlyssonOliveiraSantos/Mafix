﻿using Azure.Core.Serialization;
using Mafix.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Mafix.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            UsuarioModel usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);
            return View(usuario);
        }
    }
}
