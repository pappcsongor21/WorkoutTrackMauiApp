using SQLite;

namespace Feleves_feladat.Models
{
    class ExerciseTemplate
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int ExerciseId { get; set; } // Foreign key

        private string name;
    }
}
