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

        public void ShowAddUnit()
        {
            navigation.PushAsync(new AddUnit());
        }

        public void ShowAddAssessment()
        {
            navigation.PushAsync(new AddAssessment());
        }

        public void ShowUnitView()
        {
            navigation.PushAsync(new SubjectPage());
        }
    }
    
}
