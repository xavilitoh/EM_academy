using EM.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EM.Contorllers;


[Route("[controller]")]
public class ReportController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        var pdfMaker = new PdfHelper("https://localhost:5001/");
        var pdfFile = pdfMaker.GetPdf();
        var pdfStream = new MemoryStream(pdfFile);
        return File(pdfStream, "application/pdf", "report.pdf");
    }
}