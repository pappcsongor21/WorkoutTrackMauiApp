using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.Models
{
    public partial class Exercise : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string intensity;

        [ObservableProperty]
        private string targetReps;

        [ObservableProperty]
        private int targetSets;
    }
}
