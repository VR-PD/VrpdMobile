using RestSharp;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BlueNetScanner
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private ScannerPage scanPage;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (App.PrevPage == null)
                return base.OnBackButtonPressed();
            App.GoPageBack();
            return true;
        }

        private void BtnScan_Clicked(object sender, EventArgs e)
        {
            scanPage = new ScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                object[] data = Serializer.FromByteArray<object[]>(Convert.FromBase64String(result.Text));

                // Cast raw to QRModel
                QRModel qr = QRModel.FromArray(data);

                if (qr != null)
                    qr.UserID = "user_id";

                IRestResponse stat = Requestor.Send(qr?.ToArray());
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (stat != null && stat.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        DisplayAlert("Scanned barcode, login request send!", qr.ToString(), "OK");
                    }
                    else
                    {
                        //await DisplayPromptAsync("Login request failed", "Something went wrong.", accept: "OK");
                        DisplayAlert("Login request failed", "Something went wrong.", "OK");
                    }
                    // Switch page back
                    App.GoPageBack();
                });
            };

            App.PrevPage = Application.Current.MainPage;
            Application.Current.MainPage = scanPage;
        }
    }
}
