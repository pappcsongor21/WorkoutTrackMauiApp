using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.Models
{
    public partial class Exercise : ObservableObject
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public int WorkoutId {  get; set; } //Foreign key
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string intensity;

        [ObservableProperty]
        private string targetReps;

        [ObservableProperty]
        private int targetSets;

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

        partial void OnTargetSetsChanged(int value)
        {
            GeneratePerformedReps();
        }

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
