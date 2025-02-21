using Mafix.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Mafix.Controllers
{

    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
