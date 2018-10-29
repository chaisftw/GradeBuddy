using System;
using System.Collections.ObjectModel;
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

        public ObservableCollection<AssessmentModel> assessmentList;

        public SubjectPage()
        {
            InitializeComponent();

            TargetLabel.Text = MathUtils.ConvertPercentToGrade(SelectionManager.currentUnit.TargetGrade).ToString();
            CurrentPerc.Text = SelectionManager.currentUnit.CurrentPercent.ToString() + "%";
            CurrentLabel.Text = MathUtils.ConvertPercentToGrade(SelectionManager.currentUnit.CurrentPercent).ToString();
            TargetPerc.Text = SelectionManager.currentUnit.TargetGrade.ToString() + "%";

            assessmentList = new ObservableCollection<AssessmentModel>();

            assessList.ItemsSource = GetAssessmentList();
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

        public ObservableCollection<AssessmentModel> GetAssessmentList()
        {
            var assessEnum = App.DBManager.GetDBAssessments();

            if (assessEnum != null)
            {
                while (assessEnum.MoveNext())
                {
                    Console.WriteLine(SelectionManager.currentUnit.UnitID);
                    Console.WriteLine(assessEnum.Current.UnitID);
                    assessmentList.Add(assessEnum.Current);

                    //if (assessEnum.Current.UnitID == SelectionManager.currentUnit.UnitID)
                    //{
                    //    assessmentList.Add(assessEnum.Current);
                    //}
                }

                assessEnum.Reset();
            }

            return assessmentList;
        }
    }
}