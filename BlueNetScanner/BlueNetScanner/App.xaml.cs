using Xamarin.Forms;

namespace BlueNetScanner
{
    public partial class App : Application
    {
        private static Page prevPage;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static Page PrevPage { get => prevPage; set => prevPage = value; }

        /// <summary>
        /// This application consists of two pages without a navigation page. This method manages the <see cref="Page.OnBackButtonPressed"/> event.
        /// </summary>
        public static void GoPageBack()
        {
            // If no previous page dont do anything
            // Otherwise set previous page as main page
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
