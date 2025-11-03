using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.Models
{
    public partial class Workout : WorkoutTemplate
    {
        [ObservableProperty]
        private int length;
    }
}
