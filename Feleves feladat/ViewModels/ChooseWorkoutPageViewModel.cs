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
    public partial class ChooseWorkoutPageViewModel : ObservableObject
    {
        public ObservableCollection<Workout> Workouts { get; set; }
        private readonly IDbService db;

        public ChooseWorkoutPageViewModel(IDbService db)
        {
            this.db = db;
            Workouts = new ObservableCollection<Workout>();
            //InitalizeAsync().Wait();
            //maybe its a deadlock
        }

        [RelayCommand]
        public async Task OpenWorkoutAsync(Workout selectedWorkout)
        {
            var workout = new ShellNavigationQueryParameters()
            {
                {"workout", selectedWorkout }
            };

            await Shell.Current.GoToAsync("workout", workout);
        }

        public async Task InitializeAsync()
        {
            var workouts = await db.GetWorkoutsAsync();
            foreach (var workout in workouts)
            {
                Workouts.Add(workout);
            }
        }
    }
}
