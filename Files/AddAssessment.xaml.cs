using System;
using GradeBuddy.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GradeBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddAssessment : ContentPage
	{
		public AddAssessment ()
		{
			InitializeComponent ();
            DateTimePicker.Date = DateTime.Now;
        }

        void AddAssessmentClicked(object sender, EventArgs e)
        {
            if (NameEntry.Text == null || TotalMarksEntry.Text == null || WeightEntry.Text == null)
            {
                WarningLabel.Text = "You must enter a value into every field.";
                WarningLabel.IsVisible = true;
            }
            else
            {
                int _weight = Convert.ToInt32(WeightEntry.Text);
                int _totalMarks = Convert.ToInt32(TotalMarksEntry.Text);

                if ((_weight < 1 || _weight > 100 ) && _totalMarks < 1)
                {
                    WarningLabel.Text = "Total marks must be above 0 and weight must be between 1 and 100.";
                    WarningLabel.IsVisible = true;
                }
                else if (_weight < 1 || _weight > 100)
                {
                    WarningLabel.Text = "Weight must be between 1 and 100.";
                    WarningLabel.IsVisible = true;
                }
                else if (_totalMarks < 1)
                {
                    WarningLabel.Text = "Total marks must be above 0.";
                    WarningLabel.IsVisible = true;
                }
                else
                {
                    App.DBManager.SaveDBAssessment(new Models.AssessmentModel { AssessmentID = 0, UnitID = SelectionManager.currentUnit.UnitID, Name = NameEntry.Text, Weight = _weight, Marks = 0, CurrentPercent = 0, TotalMarks = _totalMarks, DueDate = DateTimePicker.Date, Completed = false });
                    Navigation.PopAsync();
                }
            }
        }
    }
}