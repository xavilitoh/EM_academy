using System.Diagnostics;

namespace EM.Helpers;

public class PdfHelper
{
    private string url { get; set; }
    
    public PdfHelper(string url)
    {
        this.url = url;
    }
    
    public byte[] GetPdf()
    {
       var switches = $"-q {url} -";
       
       string rotativaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rotativa", "wkhtmltopdf.exe");
       
       var processStartInfo = new ProcessStartInfo
       {
           FileName = rotativaPath,
           Arguments = switches,
           UseShellExecute = false,
           RedirectStandardOutput = true,
           CreateNoWindow = true
       };

       using var process = new Process();
       process.StartInfo = processStartInfo;
       process.Start();

       using var memoryStream = new MemoryStream();
       process.StandardOutput.BaseStream.CopyTo(memoryStream);
       process.WaitForExit();
       return memoryStream.ToArray();
    }
}