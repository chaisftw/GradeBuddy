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

            Console.WriteLine("Unit Page Loaded: UnitID = {0}", SelectionManager.currentUnit.UnitID);

            TargetLabel.Text = MathUtils.ConvertPercentToGrade(SelectionManager.currentUnit.TargetGrade).ToString();
            CurrentPerc.Text = SelectionManager.currentUnit.CurrentPercent.ToString() + "%";
            CurrentLabel.Text = MathUtils.ConvertPercentToGrade(SelectionManager.currentUnit.CurrentPercent).ToString();
            TargetPerc.Text = SelectionManager.currentUnit.TargetGrade.ToString() + "%";

            if (SelectionManager.currentUnit.CurrentPercent < 1)
            {
                CurrentLabel.Text = "--";
                CurrentPerc.Text = "--%";
            }

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
                Console.WriteLine("Populating Assessment: ");
                while (assessEnum.MoveNext())
                {
                    Console.WriteLine("AssessmentID = {0}", assessEnum.Current.AssessmentID);
                    if (assessEnum.Current.UnitID == SelectionManager.currentUnit.UnitID)
                    {
                        Console.WriteLine("Correct ID AssessmentID = {0}, ", assessEnum.Current.AssessmentID);
                        assessmentList.Add(assessEnum.Current);
                    }
                }

                assessEnum.Reset();
            }

            return assessmentList;
        }
    }
}