using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Feleves_feladat.Models
{
    public partial class Exercise : ObservableObject
    {
        //private static int nextId = 1;
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; } /*= nextId++;*/
        public int WorkoutId { get; set; } //Foreign key

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? intensity;

        [ObservableProperty]
        private string? targetReps;

        [ObservableProperty]
        private int? targetSets;

        [ObservableProperty]
        private bool isDone;

        [Ignore]
        public ObservableCollection<PerformedSet> PerformedSets { get; set; }

        public Exercise()
        {
            PerformedSets = new ObservableCollection<PerformedSet>();
        }
        public Exercise(string name, string intensity, string targetReps, int targetSets)
        {
            Name = name;
            Intensity = intensity;
            TargetReps = targetReps;
            TargetSets = targetSets;
            PerformedSets = new ObservableCollection<PerformedSet>();
            GeneratePerformedReps();
        }
        public Exercise GetDeepCopy()
        {
            return JsonSerializer.Deserialize<Exercise>(JsonSerializer.Serialize(this));
        }
        //partial void OnTargetSetsChanged(int value)
        //{
        //    GeneratePerformedReps();
        //}

        private void GeneratePerformedReps()
        {
            PerformedSets.Clear();
            for (int i = 0; i < TargetSets; i++)
            {
                PerformedSets.Add(new PerformedSet() { SetNumber = i + 1 });
            }
        }

    }
}
