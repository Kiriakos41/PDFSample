using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;

namespace PDFSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnExportPdfClicked(object sender, EventArgs e)
    {
        try
        {
            // Define the path for the generated PDF file
            string pdfFile = Path.Combine(FileSystem.AppDataDirectory, "Report.pdf");

            // Temporary font files
            string fontRegularPath = Path.Combine(FileSystem.AppDataDirectory, "OpenSans-Regular.ttf");
            string fontBoldPath = Path.Combine(FileSystem.AppDataDirectory, "OpenSans-Semibold.ttf");

            // Copy font files from app package to local storage
            await using (var stream = await FileSystem.OpenAppPackageFileAsync("OpenSans-Regular.ttf"))
            await using (var fs = File.Create(fontRegularPath))
                await stream.CopyToAsync(fs);

            await using (var stream = await FileSystem.OpenAppPackageFileAsync("OpenSans-Semibold.ttf"))
            await using (var fs = File.Create(fontBoldPath))
                await stream.CopyToAsync(fs);

            // Create PDF fonts using the copied font files
            var fontRegular = PdfFontFactory.CreateFont(fontRegularPath, PdfEncodings.IDENTITY_H);
            var fontBold = PdfFontFactory.CreateFont(fontBoldPath, PdfEncodings.IDENTITY_H);

            // Initialize PDF writer and document
            using var writer = new PdfWriter(pdfFile);
            using var pdf = new PdfDocument(writer);
            using var doc = new iText.Layout.Document(pdf);

            // Add a bold title
            doc.Add(new Paragraph("Summary of Expenses and Income")
                .SetFont(fontBold)
                .SetFontSize(18));

            // Add a regular paragraph with Greek text
            doc.Add(new Paragraph("Example with Greek text: Ταβέρνα, Μετρό, Λεωφορείο")
                .SetFont(fontRegular));

            doc.Close();

#if ANDROID
            // Copy the PDF to the Downloads folder on Android
            string downloads = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Download");
            string targetPath = Path.Combine(downloads, "Report.pdf");

            File.Copy(pdfFile, targetPath, true);
#endif

            // Open the generated PDF using the default PDF viewer
            await Launcher.Default.OpenAsync(new OpenFileRequest { File = new ReadOnlyFile(pdfFile) });
        }
        catch (Exception ex)
        {
            // Rethrow the exception for debugging purposes
            throw;
        }
    }
}
