using SQLite;
using System;

namespace GradeBuddy.Models
{
    public class UnitModel
    {
        [PrimaryKey]
        [AutoIncrement]

        public int UnitID { get; set; } // Primary Key

        public string Name { get; set; }
        public string UnitCode { get; set; }
        public string Semester { get; set; }
        public int TargetGrade { get; set; }
        public int Year { get; set; }
    }
}
