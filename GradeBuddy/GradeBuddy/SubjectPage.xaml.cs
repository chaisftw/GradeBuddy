using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GradeBuddy.Data;
using GradeBuddy.Models;

namespace GradeBuddy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectPage : ContentPage
    {
        public SubjectPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AssessmentPage
                {
                    BindingContext = e.SelectedItem as AssessmentModel
                });
            }
        }

        async void AddAssessment(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessment
            {
                BindingContext = new AssessmentModel()
            });
        }
    }
}