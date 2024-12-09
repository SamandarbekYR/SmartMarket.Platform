using Seagull.BarTender.Print;

namespace SmartMarketDeskop.Integrated.Services.Products.Print;

public class BarcodePrintService : IDisposable
{

    private Engine _btEngine;

    public BarcodePrintService()
    {
        _btEngine = new Engine();
        _btEngine.Start();
    }

    public void PrintLabel(string labelPath, string productName, string barcodeValue)
    {
        LabelFormatDocument labelFormat = _btEngine.Documents.Open(labelPath);

        labelFormat.SubStrings["ProductName"].Value = productName;
        labelFormat.SubStrings["Barcode"].Value = barcodeValue;

        labelFormat.PrintSetup.PrinterName = "XP-370B";

        labelFormat.Print();
    }

    public void Dispose()
         => GC.SuppressFinalize(this);
}
