# MauiPrintKit


MauiPrintKit is a versatile library designed to simplify printing tasks across various devices, with a current focus on Android platforms. Specifically tailored for ESC/POS printers, it currently provides seamless integration for printing images to Bluetooth printers in android. Whether you're a developer seeking to incorporate printing capabilities into your Android applications or an enthusiast looking to streamline printing processes, MauiPrintKit offers an intuitive solution.

Key Features:

* Seamless integration with Android for effortless printing tasks.
* Support for ESC/POS printers ensures compatibility across a wide range of devices.
* Simplified image printing to Bluetooth printers for enhanced convenience.
* Extensible architecture for future expansion and additional platform support.
* With MauiPrintKit, printing becomes a straightforward process, empowering developers and users alike to harness the full potential of printing technology across various environments.



In MauiProgram.cs in the CreateMauiApp add UseMauiPrintKit as shown below.
  
```C#
 
   public static MauiApp CreateMauiApp()
    {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .UseMauiPrintKit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
    }

 ```
Inject the IPrinterService
```C#
var  _printerService = App.Current!.MainPage!.Handler!.MauiContext!
    .Services.GetRequiredService<IPrintingService>();
 

 ```

  Usage (Remember to configure all bluetooth request requirements for your app.)
 ```C#
//Set your printers name, this printer must be already connected to the device.
_printerService.SetPrinter("Your Printer Name");
//Print Image 
await _printerService.PrintImageAsync(imageStream.ToArray());
  
