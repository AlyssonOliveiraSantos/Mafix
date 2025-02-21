using FastReport.Export.PdfSimple;
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
        private readonly IWebHostEnvironment _webHostEnv;
        public RelatorioController(IRelatorioRepositorio relatorioRepositorio,
                                   IWebHostEnvironment webHostEnv,
                                   IProducaoRepositorio producaoRepositorio,
                                   IMaquinaRepositorio maquinaRepositorio)
        {
            _relatorioRepositorio = relatorioRepositorio;
            _webHostEnv = webHostEnv;
            _producaoRepositorio = producaoRepositorio;
            _maquinaRepositorio = maquinaRepositorio;
        }
        public IActionResult Index()
        {
            List<MaquinaModel> maquinas = _maquinaRepositorio.BuscarTodas();
            ViewBag.Maquinas = new SelectList(maquinas, "Id", "Nome");
            return View();
        }

        [Route("CreateReport")]
        public IActionResult createReport()
        {
            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\ReportMVC.frx");
            var reportFile = caminhoReport;
            var freport = new FastReport.Report();
            var producao = _producaoRepositorio.BuscarTodos();

            freport.Dictionary.RegisterBusinessObject(producao, "producaoList", 10, true);
            freport.Report.Save(reportFile);

            return Ok($"Relatorio Gerado: {caminhoReport}");
        }

        public IActionResult visuReport(DateOnly dataInicio, DateOnly dataFim, int id)
        {
            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\ReportMVC.frx");
            var reportFile = caminhoReport;

            var freport = new FastReport.Report();
            var producao = _relatorioRepositorio.BuscarProducaoMaquinaPorData(dataInicio, dataFim, id);
            freport.Report.Load(reportFile);
            freport.Dictionary.RegisterBusinessObject(producao, "producaoList", 10, true);
            freport.Prepare();
            var pdfExport = new PDFSimpleExport();

            using MemoryStream ms = new MemoryStream();
            pdfExport.Export(freport, ms);
            ms.Flush();

            return File(ms.ToArray(), "application/pdf");
        }
    }
}
