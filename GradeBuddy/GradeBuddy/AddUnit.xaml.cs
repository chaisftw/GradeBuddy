using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GradeBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddUnit : ContentPage
	{
		public AddUnit ()
		{
			InitializeComponent ();
            SemesterPicker.SelectedIndex = 0;
            YearPicker.SelectedIndex = 0;
        }

        void AddUnitClicked(object sender, EventArgs e)
        {

            int _target = Convert.ToInt32(Target.Text);
            string _year = Convert.ToString(YearPicker.SelectedItem);
            string _semester = Convert.ToString(SemesterPicker.SelectedItem);
            if (UnitNameEntry.Text == null || UnitCodeEntry.Text == null || Target.Text == null)
            {
                WarningLabel.Text = "You must enter a value into every field.";
                WarningLabel.IsVisible = true;
            }
            else
            {
                if (_target > 100 || _target < 50)
                {
                    WarningLabel.Text = "Target grade must be between 50 and 100.";
                    WarningLabel.IsVisible = true;
                }
                else
                {
                    App.DBManager.SaveDBUnit(new Models.UnitModel { UnitID = 0, Name = UnitNameEntry.Text, UnitCode = UnitCodeEntry.Text, TargetGrade = _target, Semester = _semester, Year = _year });
                    Navigation.PopAsync();
                }
            }

        }
    }
}