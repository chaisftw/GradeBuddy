using Xamarin.Forms;

namespace GradeBuddy
{
    public class NavigationManager
    {
        private static NavigationManager instance;
        private INavigation navigation;

        private NavigationManager() { }

        public static NavigationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NavigationManager();
                }
                return instance;
            }
        }

        public INavigation Navigation
        {
            set { navigation = value; }
        }

        public void ShowOverview()
        {
            navigation.PushAsync(new MainPage());
        }

    }
    
}
