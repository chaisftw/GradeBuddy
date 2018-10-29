using System;
using System.Windows.Input;
using GradeBuddy.Models;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GradeBuddy.Data
{
    public class DBItemManager
    {
        private static object locker = new object();
        private SQLiteConnection database;

        public DBItemManager()
        {
            this.database = DependencyService.Get<ISQLite>().GetConnection();
            this.database.CreateTable<UnitModel>();
            this.database.CreateTable<AssessmentModel>();
            this.database.CreateTable<AssessmentItemModel>();
        }

        public IEnumerator<UnitModel> GetDBUnits()
        {
            lock (locker)
            {
                if (this.database.Table<UnitModel>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<UnitModel>().GetEnumerator();
                }

            }
        }

        public int SaveDBUnit(UnitModel item)
        {
            lock (locker)
            {
                if (item.UnitID != 0)
                {
                    this.database.Update(item);
                    return item.UnitID;
                }
                else
                {
                    return this.database.Insert(item);
                }
            }
        }

        public int DeleteDBUnit(int id)
        {
            lock (locker)
            {
                return this.database.Delete<UnitModel>(id);
            }
        }

        public IEnumerator<AssessmentModel> GetDBAssessments()
        {
            lock (locker)
            {
                if (this.database.Table<AssessmentModel>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<AssessmentModel>().GetEnumerator();
                }

            }
        }

        public int SaveDBAssessment(AssessmentModel item)
        {
            lock (locker)
            {
                if (item.Saved)
                {
                    this.database.Update(item);

                    item.CurrentPercent = MathUtils.CurrentAssessmentPercentage(item);

                    var unitsEnum = App.DBManager.GetDBUnits();

                    while (unitsEnum.MoveNext())
                    {
                        while (unitsEnum.Current.UnitID == item.UnitID)
                        {
                            MathUtils.CurrentUnitPercentage(unitsEnum.Current);
                            SaveDBUnit(unitsEnum.Current);
                        }
                    }

                    Console.WriteLine("Update Assessment: AssessID = {0} UnitID = {1}", item.AssessmentID, item.UnitID);
                    return item.AssessmentID;
                }
                else
                {
                    item.Saved = true;
                    this.database.Insert(item);

                    Console.WriteLine("Create Assessment: AssessID = {0} UnitID = {1}", item.AssessmentID, item.UnitID);
                    return item.AssessmentID;
                }
            }

        }

        public int DeleteDBAssessment(int id)
        {
            lock (locker)
            {
                return this.database.Delete<AssessmentModel>(id);
            }
        }

        public IEnumerator<AssessmentItemModel> GetDBAssessmentItems()
        {
            lock (locker)
            {
                if (this.database.Table<AssessmentItemModel>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<AssessmentItemModel>().GetEnumerator();
                }

            }
        }

        public int SaveDBAssessmentItem(AssessmentItemModel item)
        {
            lock (locker)
            {
                if (item.AssessmentItemID != 0)
                {
                    this.database.Update(item);
                    return item.AssessmentItemID;
                }
                else
                {
                    return this.database.Insert(item);
                }
            }
        }

        public int DeleteDBAssessmentItem(int id)
        {
            lock (locker)
            {
                return this.database.Delete<AssessmentItemModel>(id);
            }
        }

    }
}
