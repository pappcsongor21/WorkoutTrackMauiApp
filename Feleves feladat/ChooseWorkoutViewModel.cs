using CommunityToolkit.Mvvm.ComponentModel;
using Feleves_feladat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat
{
    public partial class ChooseWorkoutViewModel : ObservableObject
    {
        public ObservableCollection<Workout> Workouts { get; set; }

        public ChooseWorkoutViewModel()
        {
            Workouts = new ObservableCollection<Workout>();
            Workouts.Add(new Workout() { Name = "Upper body cali", Color = "Purple" });
            Workouts.Add(new Workout() { Name = "Lower body", Color = "Green" });
            Workouts.Add(new Workout() { Name = "Upper body cali B", Color = "Purple" });
        }


    }
}
