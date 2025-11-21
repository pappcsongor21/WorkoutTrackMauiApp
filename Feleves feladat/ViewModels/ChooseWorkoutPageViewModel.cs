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

        public ObservableCollection<Workout> WorkoutTemplates { get; private set; } = [];
        public ChooseWorkoutPageViewModel(IDbService db)
        {
            this.db = db;
        }

        [RelayCommand]
        public async Task OpenWorkoutAsync(Workout template)
        {
            var param = new ShellNavigationQueryParameters
            {
                {"workoutTemplateId", template.Id }
            };

            await Shell.Current.GoToAsync("workout", param);
        }

        [RelayCommand]
        public async Task OpenWorkoutCreatorAsync()
        {
            await Shell.Current.GoToAsync("workoutcreator");
        }

        [RelayCommand]
        public async Task GoToEditWorkoutAsync(Workout workout)
        {
            var param = new ShellNavigationQueryParameters
            {
                {"editedWorkoutTemplateId", workout.Id }
            };
            await Shell.Current.GoToAsync("editworkouttemplate", param);
        }
        [RelayCommand]
        public async Task DeleteWorkoutTemplateAsync(Workout workout)
        {
            WorkoutTemplates.Remove(workout);
            await db.DeleteWorkoutTemplateAsync(workout);
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
