

namespace MauiPrintKit;

public  partial class PrintingService 
{
    public PrintingService()
    {
        PrinterName = "Printer";
    }

    public string PrinterName { get; private set; }
    public void SetPrinter(string printerName)
    {
        if (string.IsNullOrWhiteSpace(printerName))
        {
            throw new ArgumentException("Printer name cannot be blank or empty.", nameof(printerName));
        }

        PrinterName = printerName;
    }

}
