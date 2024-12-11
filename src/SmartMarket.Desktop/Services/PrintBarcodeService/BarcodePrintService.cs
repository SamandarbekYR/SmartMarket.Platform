using Seagull.BarTender.Print;
using SmartMarket.Service.DTOs.Products.Product;
using System.IO;

namespace SmartMarket.Integrated.Services.Products.Print;

public class BarcodePrintService : IDisposable
{
    private readonly Engine _btEngine;
    private readonly string printerName = Desktop.Properties.Settings.Default.BarcodePrinterName;

    public BarcodePrintService()
    {
        _btEngine = new Engine();
        _btEngine.Start();
    }

    public void PrintLabel(FullProductDto product, int count)
    {
        string labelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Barcode.btw");

        LabelFormatDocument labelFormat = _btEngine.Documents.Open(labelPath);

        labelFormat.SubStrings["ProductName"].Value = product.Name;
        labelFormat.SubStrings["Barcode"].Value = product.Barcode;
        labelFormat.SubStrings["PCode"].Value = product.PCode;

        labelFormat.PrintSetup.PrinterName = printerName;

        for (int i = 0; i < count; i++)
        {
            labelFormat.Print();
        }
    }

    public void Dispose()
    {
        _btEngine.Stop();
        _btEngine.Dispose();
        GC.SuppressFinalize(this);
    }
}
