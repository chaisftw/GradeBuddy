using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using GradeBuddy.Data;
using GradeBuddy.Models;

namespace GradeBuddy
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<UnitModel> unitList;
        public MainPage()
        {
            InitializeComponent();

            Picker.SelectedIndex = 4;
            if (MathUtils.GPAPercentage() < 1)
            {
                CurrentPerc.Text = "--%";
                CurrentLabel.Text = "--";
            }
            else
            {
                CurrentPerc.Text = MathUtils.GPAPercentage().ToString() + "%";
                CurrentLabel.Text = MathUtils.ConvertPercentToGrade(MathUtils.GPAPercentage()).ToString();
            }
            TargetPerc.Text = MathUtils.ConvertGradeToPercent(Picker.SelectedIndex).ToString() + "%";

            unitList = new ObservableCollection<UnitModel>();
            itemList.ItemsSource = GetUnitList();
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                UnitModel unit = (UnitModel)itemList.SelectedItem;

                SelectionManager.currentUnit = unit;
                await Navigation.PushAsync(new SubjectPage
                {
                    BindingContext = e.SelectedItem as UnitModel
                });
                ((ListView)sender).SelectedItem = null;
            }
        }

        async void AddSubject(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddUnit
            {
                BindingContext = new UnitModel()
            });
        }

        public ObservableCollection<UnitModel> GetUnitList()
        {
            var unitEnum = App.DBManager.GetDBUnits();
            if(unitEnum != null)
            {
                while (unitEnum.MoveNext())
                {
                    unitList.Add(unitEnum.Current);
                }

                unitEnum.Reset();
            }

            return unitList;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            TargetPerc.Text = MathUtils.ConvertGradeToPercent(Picker.SelectedIndex).ToString() + "%";
        }
    }
}
