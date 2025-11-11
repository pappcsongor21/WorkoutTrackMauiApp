using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Services;
using System.Collections.ObjectModel;

namespace Feleves_feladat
{
    public partial class ChooseWorkoutPageViewModel
        : ObservableObject
    {
        private readonly IDbService db;
        private readonly WorkoutNavigationState navState;

        public ObservableCollection<Workout> WorkoutTemplates { get; private set; } = [];
        public ChooseWorkoutPageViewModel(IDbService db, WorkoutNavigationState navState)
        {
            this.db = db;
            this.navState = navState;
        }

        [RelayCommand]
        public async Task OpenWorkoutAsync(Workout template)
        {
            navState.SelectedWorkoutTemplate = template;

            await Shell.Current.GoToAsync("//workout");
        }
        [RelayCommand]
        public async Task OpenWorkoutCreatorAsync()
        {
            await Shell.Current.GoToAsync("workoutcreator");
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
