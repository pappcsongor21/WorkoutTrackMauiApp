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
    public partial class ChooseWorkoutPageViewModel(IDbService db) : ObservableObject
    {
        public ObservableCollection<Workout> WorkoutTemplates { get; set; } = [];
        private readonly IDbService db = db;

        [RelayCommand]
        public async Task OpenWorkoutAsync(Workout selectedWorkout)
        {
            var workout = new ShellNavigationQueryParameters()
            {
                {"workoutTemplate", selectedWorkout }
            };

            await Shell.Current.GoToAsync("workout", workout);
        }

        public async Task InitializeAsync()
        {
            WorkoutTemplates.Clear();
            var workouts = await db.GetWorkoutTemplatesAsync();
            foreach (var workout in workouts)
            {
                WorkoutTemplates.Add(workout);
            }
        }
    }
}
