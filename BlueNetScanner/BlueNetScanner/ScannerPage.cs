using ZXing.Net.Mobile.Forms;

namespace BlueNetScanner
{
    public class ScannerPage : ZXingScannerPage
    {
        protected override bool OnBackButtonPressed()
        {
            if (App.PrevPage == null)
                return base.OnBackButtonPressed();
            App.GoPageBack();
            return true;
        }
    }
}
