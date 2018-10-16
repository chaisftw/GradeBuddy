using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.ComponentModel;


namespace GradeBuddy
{
    public class ViewModel : ViewModelBase
    {
        public static NavigationManager navigationManager;

        public ViewModel()
        {
            navigationManager = NavigationManager.Instance;

        }
    }
}

