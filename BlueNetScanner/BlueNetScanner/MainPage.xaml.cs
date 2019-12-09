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
            // If theres no previous page, just run base method
            if (App.PrevPage == null)
                return base.OnBackButtonPressed();
            App.GoPageBack();
            return true;
        }

        private void BtnScan_Clicked(object sender, EventArgs e)
        {
            // Make a scan page
            scanPage = new ScannerPage();

            // Add event for when a QR code is scanned
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                // Deserialize bytes to objects
                object[] data = Serializer.FromByteArray<object[]>(Convert.FromBase64String(result.Text));

                // Cast objects to QRModel
                QRModel qr = QRModel.FromArray(data);

                if (qr != null)
                    qr.UserID = DependencyService.Get<IDevice>().GetIdentifier();

                IRestResponse stat = Requestor.Send(qr?.ToArray());
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (stat != null && stat.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        DisplayAlert("Scanned code, login request send!", "Succesfully logged in.", "OK");
                    }
                    else if (stat != null && stat.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        DisplayAlert("Login request failed", "This device is not authorized.", "OK");
                    }
                    else
                    {
                        DisplayAlert("Login request failed", "Something went wrong.", "OK");
                    }
                    // Switch page back
                    App.GoPageBack();
                });
            };

            // Set current main page as previous page
            App.PrevPage = Application.Current.MainPage;
            // Set scan page as new main page
            Application.Current.MainPage = scanPage;
        }
    }
}
