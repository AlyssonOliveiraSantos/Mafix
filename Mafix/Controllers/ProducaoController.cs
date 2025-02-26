using Mafix.DTOs;
using Mafix.Filters;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Mafix.Services;
using Mafix.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ProducaoController : Controller
    {
        private readonly IProducaoService _producaoService;


        public ProducaoController(IProducaoService producaoService)
        {
            _producaoService = producaoService;

        }


        public IActionResult Index()
        {
            List<ProducaoDTO> producoes = _producaoService.BuscarTodasProducoesDTO();
            return View(producoes);

        }

        public IActionResult Criar()
        {
            List<OperadorModel> operadores = _producaoService.BuscarTodosOperadores();
            List<MaquinaModel> maquinas = _producaoService.BuscarTodasMaquinas();
            List<ProdutoModel> produtos = _producaoService.BuscarTodosProdutos();
            List<ParadaMaquinaModel> parada = _producaoService.BuscarTodasParadasMaquinas();
            ProducaoDTO producao = new ProducaoDTO();
            ViewBag.Operadores = new SelectList(operadores, "Id", "Nome", producao.OperadorNome);
            ViewBag.Maquinas = new SelectList(maquinas, "Id", "Nome", producao.MaquinaNome);
            ViewBag.Produtos = new SelectList(produtos, "Id", "Nome", producao.ProdutoNome);
            ViewBag.Paradas = new SelectList(parada, "Id", "Codigo", producao.HoraParadaString);

            return View(producao);
        }



        [HttpPost]
        public IActionResult Criar(ProducaoDTO producao)
        {


            try
            {
                ModelState.Remove("Produto");
                ModelState.Remove("Maquina");
                ModelState.Remove("Operador");
                ModelState.Remove("ParadaMaquina");

                if (ModelState.IsValid)
                 {
                    _producaoService.AdicionarProducaoDTO(producao);
                    TempData["MensagemSucesso"] = "Produção cadastrada com sucesso!";
                    return RedirectToAction("Index");
                }

                List<OperadorModel> operadores = _producaoService.BuscarTodosOperadores();
                List<MaquinaModel> maquinas = _producaoService.BuscarTodasMaquinas();
                List<ProdutoModel> produtos = _producaoService.BuscarTodosProdutos();
                List<ParadaMaquinaModel> parada = _producaoService.BuscarTodasParadasMaquinas();
                ProducaoModel produc = new ProducaoModel();
                ViewBag.Operadores = new SelectList(operadores, "Id", "Nome", produc.OperadorId);
                ViewBag.Maquinas = new SelectList(maquinas, "Id", "Nome", produc.MaquinaId);
                ViewBag.Produtos = new SelectList(produtos, "Id", "Nome", produc.ProdutoId);
                ViewBag.Paradas = new SelectList(parada, "Id", "Codigo", produc.ParadaMaquinaId);

                return View(produc);
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos cadastrar sua produção, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Editar(int id)
        {
            List<OperadorModel> operadores = _producaoService.BuscarTodosOperadores();
            List<MaquinaModel> maquinas = _producaoService.BuscarTodasMaquinas();
            List<ProdutoModel> produtos = _producaoService.BuscarTodosProdutos();
            List<ParadaMaquinaModel> parada = _producaoService.BuscarTodasParadasMaquinas();
            ProducaoDTO producao = new ProducaoDTO();
            ViewBag.Operadores = new SelectList(operadores, "Id", "Nome", producao.OperadorId);
            ViewBag.Maquinas = new SelectList(maquinas, "Id", "Nome", producao.MaquinaId);
            ViewBag.Produtos = new SelectList(produtos, "Id", "Nome", producao.ProdutoId);
            ViewBag.Paradas = new SelectList(parada, "Id", "Codigo", producao.ParadaMaquinaId);

            ProducaoDTO producaoDto = _producaoService.BuscarTodasProducoesDTOPorId(id);
            return View(producaoDto);
        }
        [HttpPost]
        public IActionResult Alterar(ProducaoDTO producao)
        {
            try
            {
                ModelState.Remove("Produto");
                ModelState.Remove("Maquina");
                ModelState.Remove("Operador");
                ModelState.Remove("ParadaMaquina");

                if (ModelState.IsValid)
                   {
                    _producaoService.AtualizarProducaoDTO(producao);
                    TempData["MensagemSucesso"] = "Produção editada com sucesso!";
                    return RedirectToAction("Index");
                }
                return View("Editar", producao);
            }
            catch(Exception e)
            {
                TempData["MensagemError"] = $"Ops! Não conseguimos editar sua produção, tente novamente, {e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ProducaoDTO producao = _producaoService.BuscarTodasProducoesDTOPorId(id);
            return View(producao);
        }

    
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _producaoService.ApagarProducaoDTOPorId(id);

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
