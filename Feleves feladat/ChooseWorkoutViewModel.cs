using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

            Workouts.Add(new Workout() { Name = "Upper body cali", Color = "Purple",
            Exercises =
                [
                    new Exercise(){Name = "banded pullup", Intensity = "35kg band", TargetReps = "5-8", TargetSets = 3},
                    new Exercise(){Name = "ring dip hold", Intensity = "slightly assisted", TargetReps = "15s", TargetSets = 3},
                    new Exercise(){Name = "inverted row", Intensity = "-1 step", TargetReps = "8-12", TargetSets = 3},
                    new Exercise(){Name = "pushup", Intensity = "", TargetReps = "8-12", TargetSets = 3}
                ]
            });
            Workouts.Add(new Workout() { Name = "Lower body", Color = "Green" });
            Workouts.Add(new Workout() { Name = "Upper body cali B", Color = "Purple" });
        }

        [RelayCommand]
        public async Task OpenWorkoutAsync(Workout selectedWorkout)
        {
            var newPage = new WorkoutPage(selectedWorkout);

            await Application.Current.MainPage.Navigation.PushAsync(newPage);
        }
    }
}
