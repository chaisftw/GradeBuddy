using System;
using System.Collections.Generic;
using System.Text;
using GradeBuddy.Data;
using GradeBuddy.Models;

namespace GradeBuddy
{
    public static class MathUtils
    {
        public static double CurrentAssessmentPercentage(AssessmentModel item)
        {
            double CurrentPercentage = 0;
            
            if (item.Completed)
            {
                CurrentPercentage = (item.Marks / item.TotalMarks) * 100;
                        
            }

            return CurrentPercentage;
        }

        public static double CurrentUnitPercentage(UnitModel item)
        {
            double CurrentPercentage = 0;
            var assessEnum = App.DBManager.GetDBAssessments();

            while (assessEnum.MoveNext())
            {
                while (assessEnum.Current.UnitID == item.UnitID)
                {

                    CurrentPercentage += (assessEnum.Current.CurrentPercent / 100) * assessEnum.Current.Weight;
                }
            }
            CurrentPercentage = CurrentPercentage * 100;
            return Math.Floor(CurrentPercentage);
        }

        public static double GPAPercentage()
        {
            double GPAPerc = 0;
            bool complete = true;
            double percentage = 0;
            int counter = 0;
            var unitEnum = App.DBManager.GetDBUnits();
            var assessEnum = App.DBManager.GetDBAssessments();

            if (unitEnum != null)
            {
                while (unitEnum.MoveNext())
                {
                    percentage = 0;

                    if (assessEnum != null)
                    {
                        while (assessEnum.MoveNext())
                        {
                            while (complete && (assessEnum.Current.UnitID == unitEnum.Current.UnitID))
                                complete = assessEnum.Current.Completed;
                            percentage += assessEnum.Current.Weight;
                        }

                        if (percentage >= 100)
                        {
                            GPAPerc += unitEnum.Current.CurrentPercent;
                        }

                        counter++;
                    }
                }
            }

            if (counter != 0)
            {
                GPAPerc = GPAPerc / counter;
            }
            return Math.Floor(GPAPerc);
        }

        public static int ConvertPercentToGrade(double percentage)
        {
            int grade = 1;

            if (percentage >= 85)
            {
                grade = 7;
            } 
            else if (percentage >= 75)
            {
                grade = 6;
            }
            else if (percentage >= 65)
            {
                grade = 5;
            }
            else if (percentage >= 50)
            {
                grade = 4;
            }
            else if (percentage >= 40)
            {
                grade = 3;
            }
            else if (percentage >= 25)
            {
                grade = 2;
            }
            else
            {
                grade = 1;
            }

            return grade;
        }

        public static int ConvertGradeToPercent(int grade)
        {
            int percentage = 85;

            if (grade == 7)
            {
                percentage = 85;
            }
            else if (grade == 6)
            {
                percentage = 75;
            }
            else if (grade == 5)
            {
                percentage = 65;
            }
            else if (grade == 4)
            {
                percentage = 50;
            }

            return percentage;
        }
    }
}
