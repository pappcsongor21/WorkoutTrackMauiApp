using CommunityToolkit.Mvvm.ComponentModel;
using Feleves_feladat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.ViewModels
{
    public partial class WorkoutCreatorPageViewModel:ObservableObject
    {
        public ObservableCollection<Exercise> Exercises { get; set; } = [];


    }
}
