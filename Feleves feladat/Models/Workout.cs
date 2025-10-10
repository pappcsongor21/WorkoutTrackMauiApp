using CommunityToolkit.Mvvm.ComponentModel;
using Feleves_feladat.Models;

namespace Feleves_feladat
{
    public partial class Workout : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private DateTime length;

        [ObservableProperty]
        private Exercise[] exercises;

        [ObservableProperty]
        private string color;
    }
}