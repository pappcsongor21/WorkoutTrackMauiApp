using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.ViewModels
{
    [QueryProperty(nameof(EditedWorkout), "editedWorkout")]
    public partial class EditWorkoutTemplatePageViewModel(IDbService db) : ObservableObject
    {
        private readonly IDbService db = db;
        [ObservableProperty]
        private Workout editedWorkout;

        public async Task LoadExercisesFromDbAsync()
        {
            var exercises = await db.GetExercisesByWorkoutIdAsync(EditedWorkout.Id);
            foreach (var exercise in exercises)
            {
                EditedWorkout.Exercises.Add(exercise);
            }
        }
    }
}
