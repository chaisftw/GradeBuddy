using SQLite;
using System;

namespace GradeBuddy.Models
{
    public class AssessmentModel
    {
        [PrimaryKey]
        [AutoIncrement]

        public int AssessmentID { get; set; } // Primary Key

        public int UnitID; // Foreign key

        public string Name { get; set; }
        public double Weight { get; set; }
        public double Marks { get; set; }
        public double TotalMarks { get; set; }
        public double CurrentPercent { get; set; }

        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }
    }
}
