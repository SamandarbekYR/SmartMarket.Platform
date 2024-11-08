using ESC_POS_USB_NET.Printer;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDesktop.DTOs.DTOs.Transactions;
using System.Management;
using System.Text;

namespace SmartMarketDeskop.Integrated.Services.Products.Print;

public class PrintService : IDisposable
{
    private readonly string PRINTER_NAME = ""; //IdentitySingelton.GetInstance().PrinterName;
    public string printerName { get; set; } = string.Empty;
    Printer? printer;

    public PrintService()
    {
        printerName = GetSavedPrinterName();
        if (string.IsNullOrEmpty(printerName))
        {
            printerName = GetUsbPrinterName();
        }
    }

    public void Print(AddSalesRequestDto dto, List<TransactionDto> transactions)
    {
        var worker = IdentitySingelton.GetInstance().FirstName + " " + IdentitySingelton.GetInstance().LastName;
        printer = new Printer(printerName, "UTF-8");
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        printer.Separator();
        printer.AlignCenter();
        printer.DoubleWidth2();
        printer.Append("\"Smart market\"");
        printer.Separator();
        printer.Append("\n");

        printer.AlignLeft();
        printer.Append("t/r Nomi      Miqdori     Jami narxi");
        printer.Separator();

        int tr = 1;
        printer.Append("\n");
        foreach (var item in transactions)
        {
            string text = $"{tr}.  {item.Name}";
            int strLength = 25 - text.Length;
            for (int i = 1; i <= strLength; i++)
            {
                text += " ";
            }
            string temp = $"{item.Quantity}*{item.Price}";
            text += temp;
            strLength = 10 - temp.Length;
            for (int i = 0; i < strLength; i++)
            {
                text += " ";
            }
            text += item.TotalPrice.ToString();
            printer.CondensedMode(text);
            printer.Append("\n");
            tr++;
        }

        printer.Separator();
        printer.AlignLeft();
        printer.Append("\n");
        printer.Append($"Naqd:                         {dto.CashSum} so'm");
        printer.Append($"Plastik:                      {dto.CardSum} so'm");
        printer.Append($"Chegirma:                     {dto.DiscountSum} so'm");
        printer.Append($"Jami summa:                   {dto.TotalCost} so'm");
        printer.Append("\n");

        printer.Separator();
        printer.AlignLeft();
        printer.Append("\n");
        printer.Append($"Sotuvchi:               {worker}");
        printer.AlignRight();
        printer.Append("\n");
        printer.AlignLeft();
        printer.Append("Sana:                      " + DateTime.Now.ToString());

        printer.Append("\n");
        printer.AlignCenter();
        printer.Append("\n");

        printer.AlignCenter();
        printer.BoldMode("Xaridingiz uchun tashakkur!");

        printer.Append("\n");
        printer.Separator();

        printer.FullPaperCut();
        printer.PrintDocument();
    }

    public void Test()
    {
        printer = new Printer(printerName, "UTF-8");
        Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        printer.Separator();
        printer.AlignCenter();
        printer.Append("\n");
        printer.BoldMode("Test printer!");
        printer.Append("\n");
        printer.Separator();
        printer.FullPaperCut();
        printer.PrintDocument();
    }

    public string GetSavedPrinterName()
    {
        try
        {
            StreamReader streamReader = new StreamReader(PRINTER_NAME);
            string res = streamReader.ReadToEnd();
            streamReader.Close();
            return res;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public void SavePrinter(string name)
    {
        StreamWriter sw = new StreamWriter(PRINTER_NAME);
        sw.Write(name);
        sw.Flush();
        sw.Close();
    }

    public List<string> ConnectedPrinters()
    {
        List<string> printers = new List<string>();
        foreach (var print in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
        {
            printers.Add(print.ToString()!);
        }

        return printers;
    }

    public string GetUsbPrinterName()
    {
        try
        {
            var usbPrinters = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer WHERE PortName LIKE 'USB%'");
            foreach (ManagementObject printer in searcher.Get())
            {
                usbPrinters.Add(printer["Name"].ToString()!);
            }

            if (usbPrinters.Count > 0)
            {
                SavePrinter(usbPrinters[0]); 
                return usbPrinters[0]; 
            }

            return string.Empty;
        }
        catch 
        {
            return string.Empty;
        }
    }

    public void Dispose()
         => GC.SuppressFinalize(this);
}

