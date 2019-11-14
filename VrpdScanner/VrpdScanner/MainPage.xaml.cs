using RestSharp;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace VrpdScanner
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private ZXingScannerPage scanPage;

        public MainPage()
        {
            InitializeComponent();

            buttonScanOverlay.Clicked += OnOverlayClicked;
        }

        private async void OnOverlayClicked(object sender, EventArgs e)
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                object[] data = Serializer.FromByteArray<object[]>(Convert.FromBase64String(result.Text));
                object[] dataOut = new object[3];
                try
                {
                    dataOut[QRData.Keynum] = data[QRData.Keynum];
                    dataOut[QRData.Created] = data[QRData.Created];
                    dataOut[QRData.UserID] = "test user";
                }
                catch (IndexOutOfRangeException)
                {
                }

                IRestResponse stat = Requestor.Send(dataOut);

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

            await Navigation.PushAsync(scanPage).ConfigureAwait(false);
        }
    }
}
