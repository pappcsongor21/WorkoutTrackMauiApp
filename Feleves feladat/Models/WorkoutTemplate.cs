using CommunityToolkit.Mvvm.ComponentModel;
using Feleves_feladat.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace Feleves_feladat
{
    public partial class WorkoutTemplate : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        [property: Ignore]
        private ObservableCollection<Exercise> exercises = [];

        [ObservableProperty]
        private string color;
    }
}