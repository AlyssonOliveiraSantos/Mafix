﻿using FastReport.Export.PdfSimple;
using Mafix.Models;
using Mafix.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mafix.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IRelatorioRepositorio _relatorioRepositorio;
        private readonly IProducaoRepositorio _producaoRepositorio;
        private readonly IMaquinaRepositorio _maquinaRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;
        private readonly IWebHostEnvironment _webHostEnv;
        public RelatorioController(IRelatorioRepositorio relatorioRepositorio,
                                   IWebHostEnvironment webHostEnv,
                                   IProducaoRepositorio producaoRepositorio,
                                   IMaquinaRepositorio maquinaRepositorio,
                                   IOperadorRepositorio operadorRepositorio)
        {
            _relatorioRepositorio = relatorioRepositorio;
            _webHostEnv = webHostEnv;
            _producaoRepositorio = producaoRepositorio;
            _maquinaRepositorio = maquinaRepositorio;
            _operadorRepositorio = operadorRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MaquinasPorDataReport()
        {

            List<MaquinaModel> maquinas = _maquinaRepositorio.BuscarTodas();
            ViewBag.Maquinas = new SelectList(maquinas, "Id", "Nome");
            return View();
        }

        public IActionResult OperadoresPorDataReport()
        {
            List<OperadorModel> operador = _operadorRepositorio.BuscarTodos();
            ViewBag.Operadores = new SelectList(operador, "Id", "Nome");
            return View();
        }

        [Route("CreateReport")]
        public IActionResult createReport()
        {
            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\MaquinaProducaoGeral.frx");
            var reportFile = caminhoReport;
            var freport = new FastReport.Report();
            DateOnly dateOnly = new DateOnly();
            DateOnly dateOnly1 = new DateOnly();
            int inteiro = 0;

            var producao = _relatorioRepositorio.BuscarProducaoOperadorPorData(dateOnly, dateOnly1, inteiro);

            freport.Dictionary.RegisterBusinessObject(producao, "producaoList", 10, true);
            freport.Report.Save(reportFile);

            return Ok($"Relatorio Gerado: {caminhoReport}");
        }

        public IActionResult OperadorProducaoReport(DateOnly dataInicio, DateOnly dataFim, int id)
        {
            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\Operador.frx");
            var reportFile = caminhoReport;

            var freport = new FastReport.Report();
            var producoes = _relatorioRepositorio.BuscarProducaoOperadorPorData(dataInicio, dataFim, id);
            double mediaEficiencia = 0;
            int quantidadeEficiencia = 0;

            foreach (var producao in producoes) 
            {
                mediaEficiencia += producao.Eficiencia;
                quantidadeEficiencia++;
                producao.MediaEficiencia = mediaEficiencia / quantidadeEficiencia;
            }
             


            freport.Report.Load(reportFile);
            freport.Dictionary.RegisterBusinessObject(producoes, "producaoList", 10, true);
            freport.Prepare();
            var pdfExport = new PDFSimpleExport();

            using MemoryStream ms = new MemoryStream();
            pdfExport.Export(freport, ms);
            ms.Flush();

            return File(ms.ToArray(), "application/pdf");
        }

        public IActionResult MaquinasProducaoReport(DateOnly dataInicio, DateOnly dataFim, int id)
        {
            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\Maquina.frx");
            var reportFile = caminhoReport;

            var freport = new FastReport.Report();
            var producoes = _relatorioRepositorio.BuscarProducaoMaquinaPorData(dataInicio, dataFim, id);
            double mediaEficiencia = 0;
            int quantidadeEficiencia = 0;
            foreach (var producao in producoes)
            {
                mediaEficiencia += producao.Eficiencia;
                quantidadeEficiencia++;
                producao.MediaEficiencia = mediaEficiencia / quantidadeEficiencia;
            }


            freport.Report.Load(reportFile);
            freport.Dictionary.RegisterBusinessObject(producoes, "producaoList", 10, true);
            freport.Prepare();
            var pdfExport = new PDFSimpleExport();

            using MemoryStream ms = new MemoryStream();
            pdfExport.Export(freport, ms);
            ms.Flush();

            return File(ms.ToArray(), "application/pdf");
        }

        public IActionResult MaquinasProducaoGeralReport()
        {

            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\MaquinaProducaoGeral.frx");
            var reportFile = caminhoReport;

            var freport = new FastReport.Report();
            var producoes = _relatorioRepositorio.BuscarProducaoGeralMesMaquina();

            freport.Report.Load(reportFile);
            freport.Dictionary.RegisterBusinessObject(producoes, "producaoList", 10, true);
            freport.Prepare();
            var pdfExport = new PDFSimpleExport();

            using MemoryStream ms = new MemoryStream();
            pdfExport.Export(freport, ms);
            ms.Flush();

            return File(ms.ToArray(), "application/pdf");
        }
    }
}
