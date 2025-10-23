using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
