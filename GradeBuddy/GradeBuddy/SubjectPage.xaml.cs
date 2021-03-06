﻿using System;
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
        AssessmentModel assessItem;

        public ObservableCollection<AssessmentModel> assessmentList;

        public SubjectPage()
        {
            InitializeComponent();


        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                AssessmentModel assessment = (AssessmentModel)assessList.SelectedItem;

                SelectionManager.currentAssessment = assessment;
                assessItem = e.SelectedItem as AssessmentModel;

                await Navigation.PushAsync(new AssessmentPage(assessItem));
                ((ListView)sender).SelectedItem = null;
            }
        }

        async void AddAssessment(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessment
            {
                BindingContext = new AssessmentModel()
            });
        }

        async void DeleteUnit(object sender, EventArgs e)
        {
            var assessEnum = App.DBManager.GetDBAssessments();

            if (assessEnum != null)
            {
                while (assessEnum.MoveNext())
                {
                    if (assessEnum.Current.UnitID == SelectionManager.currentUnit.UnitID)
                    {
                        App.DBManager.DeleteDBAssessment(assessEnum.Current.AssessmentID);
                    }
                }
            }

            App.DBManager.DeleteDBUnit(SelectionManager.currentUnit.UnitID);
            await Navigation.PopAsync();
        }

        public ObservableCollection<AssessmentModel> GetAssessmentList()
        {
            var assessEnum = App.DBManager.GetDBAssessments();

            if (assessEnum != null)
            {
                while (assessEnum.MoveNext())
                {
                    if (assessEnum.Current.UnitID == SelectionManager.currentUnit.UnitID)
                    {
                        assessmentList.Add(assessEnum.Current);
                    }
                }

                assessEnum.Reset();
            }

            return assessmentList;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            assessList.ItemsSource = null;
            assessmentList = new ObservableCollection<AssessmentModel>();
            assessList.ItemsSource = GetAssessmentList();

            TargetLabel.Text = MathUtils.ConvertPercentToGrade(SelectionManager.currentUnit.TargetGrade).ToString();
            CurrentPerc.Text = SelectionManager.currentUnit.CurrentPercent.ToString() + "%";
            CurrentLabel.Text = MathUtils.ConvertPercentToGrade(SelectionManager.currentUnit.CurrentPercent).ToString();
            TargetPerc.Text = SelectionManager.currentUnit.TargetGrade.ToString() + "%";
            if (SelectionManager.currentUnit.CurrentPercent < 1)
            {
                CurrentLabel.Text = "--";
                CurrentPerc.Text = "--%";
            }
        }
    }
}