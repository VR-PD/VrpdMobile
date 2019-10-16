using RestSharp;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace VrpdScanner
{
    public class ScanPage : ContentPage
    {
        private Button buttonScanDefaultOverlay;
        private ZXingScannerPage scanPage;

        public ScanPage() : base()
        {
            buttonScanDefaultOverlay = new Button
            {
                Text = "Scan barcode",
                AutomationId = "scanWithDefaultOverlay",
            };
            buttonScanDefaultOverlay.Clicked += async delegate
            {
                scanPage = new ZXingScannerPage();
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;

                    IRestResponse stat = Requestor.Send(result.Text);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (stat.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            Navigation.PopAsync();
                            DisplayAlert("Scanned barcode, login request send!", result.Text, "OK");
                        }
                        else
                        {
                            Navigation.PopAsync();
                            DisplayAlert("Login request failed", "Something went wrong.", "OK");
                        }
                    });
                };

                await Navigation.PushAsync(scanPage);
            };

            var stack = new StackLayout();
            stack.Children.Add(buttonScanDefaultOverlay);

            Content = stack;
        }
    }
}
