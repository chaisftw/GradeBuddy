using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GradeBuddy.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GradeBuddy
{
    public partial class App : Application
    {
        private ContentPage mainPage;
        private static DBItemManager dbManager;
        public App()
        {
            InitializeComponent();
            mainPage = new MainPage();
            MainPage = new NavigationPage(mainPage);
            NavigationManager.Instance.Navigation = MainPage.Navigation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static DBItemManager DBManager
        {
            get
            {
                if (dbManager == null)
                {
                    dbManager = new DBItemManager();
                }
                return dbManager;
            }
        }
    }
}
