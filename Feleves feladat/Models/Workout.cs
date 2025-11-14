using CommunityToolkit.Mvvm.ComponentModel;
using Feleves_feladat.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace Feleves_feladat
{
    public partial class Workout : ObservableObject
    {
        private static int nextId = 0;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        [property: Ignore]
        private ObservableCollection<Exercise> exercises = [];

        [ObservableProperty]
        private string color;

        public bool IsTemplate { get; set; }

        [ObservableProperty]
        private int? length;

        [ObservableProperty]
        private DateTime? date;

        [ObservableProperty]
        private string imageUrl;

        [ObservableProperty]
        private bool hasImage;
    }
}