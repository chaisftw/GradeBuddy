using SQLite;

namespace GradeBuddy.Models
{
    public class AssessmentItemModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int AssessmentItemID { get; set; } // Foreign key

        public int AssessmentID { get; set; } // Foreign key
        
        public string Name { get; set; }
        public int Marks { get; set; }
    }
}
