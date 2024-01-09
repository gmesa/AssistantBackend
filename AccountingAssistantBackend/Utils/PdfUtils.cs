using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text;

namespace AccountingAssistantBackend.Utils
{
    public static class PdfUtils
    {
        public static string GetPdfText(MemoryStream stream)
        {
            stream.Position = 0;
            PdfReader reader = new PdfReader(stream);
            PdfDocument pdf = new PdfDocument(reader);
            StringBuilder text = new StringBuilder();
            for (int page = 1; page <= pdf.GetNumberOfPages(); page++)
            {
                text.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(page)));
            }
            reader.Close();
            return text.ToString();
        }
    }
}
