using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat
{
    [QueryProperty(nameof(Workout),"workout")]
    public partial class WorkoutPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private Workout workout;
    }
}
