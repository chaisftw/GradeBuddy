﻿using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xamarin.Forms;
using System;
using GradeBuddy.Models;
using GradeBuddy.Data;

namespace GradeBuddy
{
    public class ViewModel : ViewModelBase
    {
        public static NavigationManager navigationManager;

        private ObservableCollection<UnitModel> unitList = new ObservableCollection<UnitModel>();
        private ObservableCollection<AssessmentModel> assessmentList = new ObservableCollection<AssessmentModel>();
        private ObservableCollection<AssessmentItemModel> assessmentItemList = new ObservableCollection<AssessmentItemModel>();

        // Current selection properties
        public UnitModel CurrentUnit { get; set; }
        public AssessmentModel CurrentAssessment { get; set; }
        public AssessmentItemModel CurrentAssessmentItem { get; set; }

        // Add unit Properties
        public string UnitNameEntry { get; set; }
        public string UnitCodeEntry { get; set; }
        public string TargetEntry { get; set; }
        public string YearEntry { get; set; }
        public string SemesterEntry { get; set; }

        // Add assessment properties
        public string AssessmentNameEntry { get; set; }
        public double WeightEntry { get; set; }
        public double MarksEntry { get; set; }
        public double TotalMarksEntry { get; set; }
        public DateTime DateEntry { get; set; }
        public bool CompletedEntry { get; set; }

        // Add Item Properties
        public string ItemNamEntry { get; set; }
        public string ItemTotalMarksEntry { get; set; }

        // Database commands
        public ICommand AddUnitCommand { protected set; get; }
        public ICommand AddAssessmentCommand { protected set; get; }
        public ICommand AddItemCommand { protected set; get; }
        public ICommand DeleteUnitCommand { protected set; get; }
        public ICommand DeleteAssessmentCommand { protected set; get; }

        public ViewModel()
        {
            navigationManager = NavigationManager.Instance;

            // Adds a new unit to the database, then navigates to the sub menu
            AddUnitCommand = new Command(() =>
            {
                Console.WriteLine("Unit Button Detect");
                int targetInt;
                int yearInt;

                try
                {
                    targetInt = Convert.ToInt32(TargetEntry);
                    yearInt = Convert.ToInt32(YearEntry);
                }
                catch (Exception)
                {
                    targetInt = 2222; //TO DO: add error text labels
                    yearInt = 2222;
                }
                
                App.DBManager.SaveDBUnit(new Models.UnitModel { UnitID = 0, Name = UnitNameEntry, UnitCode = UnitCodeEntry, TargetGrade = targetInt, Semester = SemesterEntry, Year = YearEntry});
                navigationManager.ShowOverview();
            });

            AddAssessmentCommand = new Command(() =>
            {
                double weightInt;
                double marksInt;

                try
                {
                    weightInt = Convert.ToInt32(WeightEntry);
                    marksInt = Convert.ToInt32(TotalMarksEntry);
                }
                catch (Exception)
                {
                    weightInt = 0;
                    marksInt = 0;
                }

                App.DBManager.SaveDBAssessment(new Models.AssessmentModel { AssessmentID = 0, UnitID = SelectionManager.currentUnit.UnitID, Name = AssessmentNameEntry, Weight = weightInt, Marks = MarksEntry, CurrentPercent = 0, TotalMarks = TotalMarksEntry, DueDate = DateEntry, Completed = CompletedEntry });
                navigationManager.ShowUnitView();
            });

            AddItemCommand = new Command(() =>
            {
                int totalMarks;

                try
                {
                    totalMarks = Convert.ToInt32(ItemTotalMarksEntry);
                }
                catch (Exception)
                {
                    totalMarks = 22;
                }

                App.DBManager.SaveDBAssessmentItem(new Models.AssessmentItemModel { AssessmentItemID = 0, AssessmentID = CurrentAssessment.AssessmentID, Name = ItemNamEntry, TotalMarks = totalMarks, MarksAchieved = 0, Complete = false });
                navigationManager.ShowAddAssessment();
            });

            DeleteUnitCommand = new Command(() =>
            {
                var assessEnum = App.DBManager.GetDBAssessments();

                if (assessEnum != null)
                {
                    while (assessEnum.MoveNext() == (assessEnum != null))
                    {
                        if (assessEnum.Current.UnitID == SelectionManager.currentUnit.UnitID)
                        {
                            App.DBManager.DeleteDBAssessment(assessEnum.Current.AssessmentID);
                        }
                    }
                }

                App.DBManager.DeleteDBUnit(SelectionManager.currentUnit.UnitID);
                navigationManager.ShowOverview();
            });

            DeleteAssessmentCommand = new Command(() =>
            {
                App.DBManager.DeleteDBAssessment(SelectionManager.currentAssessment.AssessmentID);
                //navigationManager.ShowSubjectPage();
                
            });
        }

        public ObservableCollection<UnitModel> GetUnitList()
        {
            var unitEnum = App.DBManager.GetDBUnits();

            while (unitEnum.MoveNext())
            {
                this.unitList.Add(unitEnum.Current);
            }

            unitEnum.Reset();

            return unitList;
        }

        public ObservableCollection<AssessmentModel> GetAssessmentList()
        {
            var assessEnum = App.DBManager.GetDBAssessments();
            while (assessEnum.MoveNext())
            {
                if(assessEnum.Current.UnitID == CurrentUnit.UnitID)
                {
                    this.assessmentList.Add(assessEnum.Current);
                }
                
            }

            assessEnum.Reset();

            return assessmentList;
        }

        public ObservableCollection<AssessmentItemModel> GetAssessmentItemList()
        {
            var itemEnum = App.DBManager.GetDBAssessmentItems();
            while (itemEnum.MoveNext())
            {
                if (itemEnum.Current.AssessmentID == CurrentAssessment.AssessmentID)
                {
                    this.assessmentItemList.Add(itemEnum.Current);
                }

            }

            itemEnum.Reset();

            return assessmentItemList;
        }
    }
}

