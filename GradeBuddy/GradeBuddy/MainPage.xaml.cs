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
            unitList = new ObservableCollection<UnitModel>();
            

            itemList.ItemsSource = GetUnitList();
        }

        protected override async void OnAppearing()
        {

        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new SubjectPage
                {
                    BindingContext = e.SelectedItem as UnitModel
                });
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

            while (unitEnum.MoveNext())
            {
                unitList.Add(unitEnum.Current);
            }

            unitEnum.Reset();

            return unitList;
        }


    }
}
