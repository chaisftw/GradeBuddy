using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GradeBuddy.Models;
using GradeBuddy.Data;

namespace GradeBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssessmentPage : ContentPage
	{
        AssessmentModel assessItem;

        public AssessmentPage (AssessmentModel assessPassed)
		{
			InitializeComponent ();
            assessItem = assessPassed;
            TitleLabel.Text = SelectionManager.currentUnit.Name + ": " + SelectionManager.currentAssessment.Name;
            Console.WriteLine(assessPassed.AssessmentID);
        }

        void UpdateAssessment(object sender, EventArgs e)
        {
            if (int.TryParse(Marks.Text, out int n) && (n <= SelectionManager.currentAssessment.TotalMarks))
            {
                assessItem.Marks = n;
                assessItem.Completed = true;
                SelectionManager.currentAssessment = assessItem;

                App.DBManager.SaveDBAssessment(assessItem);
                Navigation.PopAsync();
            }
            else
            {
                WarningLabel.IsVisible = true;
            }
        }

        void DeleteAssessment(object sender, EventArgs e)
        {
            App.DBManager.DeleteDBAssessment(SelectionManager.currentAssessment.AssessmentID);
            Navigation.PopAsync();
        }
    }
}