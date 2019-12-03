using Xamarin.Forms;

namespace BlueNetScanner
{
    public partial class App : Application
    {
        public static Page PrevPage;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static void GoPageBack()
        {
            if (PrevPage != null)
                Current.MainPage = PrevPage;
            PrevPage = null;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }
    }
}
