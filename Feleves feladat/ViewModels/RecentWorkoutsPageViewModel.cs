using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace Feleves_feladat.ViewModels
{
    public partial class RecentWorkoutsPageViewModel(IDbService db) : ObservableObject
    {
        private readonly IDbService db = db;
        public ObservableCollection<Workout> Workouts { get; set; } = [];
        public async Task LoadWorkoutsFromDbAsync()
        {
            Workouts.Clear();
            var ws = await db.GetWorkoutsAsync();
            foreach (var workout in ws)
            {
                var es = await db.GetExercisesByWorkoutIdAsync(workout.Id);
                es.ForEach(e => workout.Exercises.Add(e));
                Workouts.Add(workout);
            }
        }
        [RelayCommand]
        public async Task ShareWorkoutAsync(Workout workout)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await Share.Default.RequestAsync(new ShareTextRequest()
                {
                    Title = "Share ToDo",
                    Text = workout.ToString()
                });
            }
        }

    }
}
