//using Seagull.BarTender.Print;
//using SmartMarket.Service.DTOs.Products.Product;
//using System.IO;

//namespace SmartMarket.Integrated.Services.Products.Print;

//public class BarcodePrintService : IDisposable
//{
//    private readonly Engine _btEngine;
//    private readonly string printerName = Desktop.Properties.Settings.Default.BarcodePrinterName;

//    public BarcodePrintService()
//    {
//        _btEngine = new Engine();
//        _btEngine.Start();
//    }

//    public void PrintLabel(FullProductDto product, int count)
//    {
//        string labelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Barcode.btw");

//        LabelFormatDocument labelFormat = _btEngine.Documents.Open(labelPath);

//        labelFormat.SubStrings["ProductName"].Value = product.Name;
//        labelFormat.SubStrings["Barcode"].Value = product.Barcode; 
//        labelFormat.SubStrings["PCode"].Value = product.PCode; 

//        labelFormat.PrintSetup.PrinterName = printerName;

//        for (int i = 0; i < count; i++)
//        {
//            labelFormat.Print();
//        }
//    }

//    public void Dispose()
//    {
//        _btEngine.Stop();
//        _btEngine.Dispose();
//        GC.SuppressFinalize(this);
//    }
//}

//using System;
//using System.Drawing;
//using System.Drawing.Printing;
//using System.Windows;
//using ZXing;
//using ZXing.Common;
//using ZXing.Windows.Compatibility;

//namespace BarcodePrinter
//{
//    public partial class MainWindow : Window
//    {
//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void PrintButton_Click(object sender, RoutedEventArgs e)
//        {
//            string input = InputTextBox.Text;
//            if (!string.IsNullOrEmpty(input))
//            {
//                PrintBarcode(input);
//            }
//            else
//            {
//                MessageBox.Show("Please enter a number.");
//            }
//        }

//        private void PrintBarcode(string data)
//        {
//            BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>
//            {
//                Format = BarcodeFormat.CODE_128,
//                Options = new EncodingOptions
//                {
//                    Height = 100,
//                    Width = 300
//                },
//                Renderer = new BitmapRenderer()
//            };

//            using (Bitmap bitmap = writer.Write(data))
//            {
//                PrintDocument printDoc = new PrintDocument();
//                printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custom", 118, 79);
//                printDoc.PrintPage += (s, e) =>
//                {
//                    int x = (e.PageBounds.Width - bitmap.Width) / 2;
//                    int y = (e.PageBounds.Height - bitmap.Height) / 2;
//                    e.Graphics!.DrawImage(bitmap, x, y);
//                };
//                printDoc.Print();
//            }
//        }
//    }
//}
