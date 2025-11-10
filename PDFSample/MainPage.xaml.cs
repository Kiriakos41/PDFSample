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


            string pdfFile = Path.Combine(FileSystem.AppDataDirectory, "Report.pdf");

            // Προσωρινά αρχεία για fonts
            string fontRegularPath = Path.Combine(FileSystem.AppDataDirectory, "OpenSans-Regular.ttf");
            string fontBoldPath = Path.Combine(FileSystem.AppDataDirectory, "OpenSans-Semibold.ttf");

            // Αντιγραφή από πόρους
            await using (var stream = await FileSystem.OpenAppPackageFileAsync("OpenSans-Regular.ttf"))
            await using (var fs = File.Create(fontRegularPath))
                await stream.CopyToAsync(fs);

            await using (var stream = await FileSystem.OpenAppPackageFileAsync("OpenSans-Semibold.ttf"))
            await using (var fs = File.Create(fontBoldPath))
                await stream.CopyToAsync(fs);

            // Δημιουργία γραμματοσειρών
            var fontRegular = PdfFontFactory.CreateFont(fontRegularPath, PdfEncodings.IDENTITY_H);
            var fontBold = PdfFontFactory.CreateFont(fontBoldPath, PdfEncodings.IDENTITY_H);

            using var writer = new PdfWriter(pdfFile);
            using var pdf = new PdfDocument(writer);
            using var doc = new iText.Layout.Document(pdf);

            doc.Add(new Paragraph("Σύνοψη Εξόδων και Εσόδων").SetFont(fontBold).SetFontSize(18));
            doc.Add(new Paragraph("Παράδειγμα με ελληνικά: Ταβέρνα, Μετρό, Λεωφορείο").SetFont(fontRegular));

            doc.Close();

#if ANDROID
            string downloads = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Download");

            string targetPath = Path.Combine(downloads, "Report.pdf");

            File.Copy(pdfFile, targetPath, true);
#endif
            await Launcher.Default.OpenAsync(new OpenFileRequest { File = new ReadOnlyFile(pdfFile) });
        }
        catch (Exception ex)
        {

            throw;
        }
    }

}
