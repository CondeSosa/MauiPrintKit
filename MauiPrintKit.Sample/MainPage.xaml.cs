using Microsoft.Extensions.DependencyInjection;

namespace MauiPrintKit.Sample
{
    public partial class MainPage : ContentPage
    {
       
        private readonly IPrintingService _printerService;
        public MainPage()
        {
            InitializeComponent();

            _printerService = App.Current!.MainPage!.Handler!.MauiContext!
                .Services.GetRequiredService<IPrintingService>();
        }

        private async void PrintClicked(object sender, EventArgs e)
        {


            var statusBlietooth = Permissions.CheckStatusAsync<Permissions.Bluetooth>();

            if (statusBlietooth.Result != PermissionStatus.Granted)
            {
                statusBlietooth = Permissions.RequestAsync<Permissions.Bluetooth>();
            }

            if (statusBlietooth.Result == PermissionStatus.Granted)
            {

                //We take a screenshot of the layout we want to print.
                var result = await gridView.CaptureAsync();
                //convert it to a memory stream
                using MemoryStream imageStream = new MemoryStream();
                await result.CopyToAsync(imageStream);

                //Set out printers name, this printer must be already connected to the device.
                _printerService.SetPrinter("Printer_27D9");

                //Print Image 
                await _printerService.PrintImageAsync(imageStream.ToArray());
            }
        }
    }

}
