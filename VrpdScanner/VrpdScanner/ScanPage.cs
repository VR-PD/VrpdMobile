using RestSharp;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

                await Navigation.PushAsync(scanPage);
            };

            var stack = new StackLayout();
            stack.Children.Add(buttonScanDefaultOverlay);

            Content = stack;
        }
    }
}
