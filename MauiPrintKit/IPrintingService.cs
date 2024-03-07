using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiPrintKit;

public interface IPrintingService
{
    public string PrinterName { get; }
    void SetPrinter(string printerName);
    Task PrintImageAsync(byte[] imageArray);
  
    
}
