using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace Feleves_feladat.Models
{
    public partial class PerformedSet : ObservableObject
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id {  get; set; }

        public int ExerciseId {  get; set; } //Foreign key
        [ObservableProperty]
        private int setNumber;

        [ObservableProperty]
        private int repCount;

        [ObservableProperty]
        private int repsInReserve; // or difficulty
    }
}
