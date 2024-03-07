using Android.Bluetooth;
using Java.Util;
using SkiaSharp;
using System.Text;


namespace MauiPrintKit;

public  partial  class PrintingService : IPrintingService
{

    public  async Task PrintImageAsync(byte[] imageArray)
    {
        if(imageArray == null)
        {
            throw new Exception($"Needs a valid {nameof(imageArray)}");
        }

        using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
        {
            if (bluetoothAdapter == null)
            {
                // Bluetooth is not supported on this device
                throw new Exception($"Bluetooth may not be supported on this device.");
            }

            BluetoothDevice? device = (from bd in bluetoothAdapter?.BondedDevices
                                       where bd?.Name == PrinterName
                                       select bd).FirstOrDefault();
            if (device == null)
            {
                throw new Exception($"No printer found with the name: {PrinterName}");
            }

            try
            {
                using (BluetoothSocket? bluetoothSocket =
                    device!.CreateRfcommSocketToServiceRecord(
                    UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                {
                    if (bluetoothSocket == null)
                    {
                        throw new Exception($"Error configuring the printer Socket.");
                    }

                   using MemoryStream stream = new MemoryStream();
                    try
                    {
                        bluetoothSocket?.Connect();

                        var imageManager = new ImageManager();

                        //Format image
                        SKBitmap bitmap = imageManager.ByteArrayToSKBitmap(imageArray);
                        var image = imageManager.GetImage(bitmap);
                        stream.Write(image);

                        //Cut paper ESC/POS
                        stream.Write(GetCutCommand());

                        byte[] bytes = stream.GetBuffer();

                       await bluetoothSocket!.OutputStream!.WriteAsync(bytes, 0, bytes.Length);

                
                       
                    }
                    finally
                    {
                        bluetoothSocket?.Close();
                        stream.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error printing image: {ex.Message}");
            }
        }
    }

    public  byte[] GetCutCommand()
    {
        // ESC/POS command sequence for full paper cut
        string ESC = Convert.ToString((char)27);
        string GS = Convert.ToString((char)29);
        string COMMAND = ESC + "@" + GS + "V" + (char)48;

        // Convert command string to bytes
       return Encoding.ASCII.GetBytes(COMMAND);

    }

}
