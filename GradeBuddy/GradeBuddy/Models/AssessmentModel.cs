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
        public int Weight { get; set; }
        public int Marks { get; set; }
        public bool MultiItem { get; set; }
        public DateTime DueDate { get; set; }
    }
}
