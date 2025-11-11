using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat
{
    public partial class ChooseWorkoutPageViewModel(IDbService db, WorkoutNavigationState navState) : ObservableObject
    {
        public ObservableCollection<Workout> WorkoutTemplates { get; set; } = [];
        private readonly IDbService db = db;
        private readonly WorkoutNavigationState navState = navState;

        [RelayCommand]
        public async Task OpenWorkoutAsync(Workout template)
        {
            navState.SelectedWorkoutTemplate = template;

            await Shell.Current.GoToAsync("//workout");
        }
        [RelayCommand]
        public async Task OpenWorkoutCreatorAsync()
        {
            await Shell.Current.GoToAsync("//workoutcreator");
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
