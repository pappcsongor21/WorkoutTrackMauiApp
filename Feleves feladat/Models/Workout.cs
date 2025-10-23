using CommunityToolkit.Mvvm.ComponentModel;
using Feleves_feladat.Models;
using SQLite;

namespace Feleves_feladat
{
    public partial class Workout : ObservableObject
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private DateTime length;

        [ObservableProperty]
        [property:Ignore]
        private Exercise[] exercises;

        [ObservableProperty]
        private string color;
    }
}