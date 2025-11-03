using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat
{
    [QueryProperty(nameof(WorkoutTemplate),"workoutTemplate")]
    public partial class WorkoutPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private WorkoutTemplate workoutTemplate;

        [ObservableProperty]
        private Workout realizedWorkout;

        IDbService db;
        public WorkoutPageViewModel(IDbService db)
        {
            this.db = db;
        }
        public async Task InitializeExercisesFromDb()
        {
            WorkoutTemplate.Exercises.Clear();
            var exercises = await db.GetExercisesByWorkoutIdAsync((int)WorkoutTemplate.Id);
            RealizedWorkout = new()
            {
                Name = WorkoutTemplate.Name,
                Color = WorkoutTemplate.Color
            };
            foreach (var exercise in exercises)
            {
                WorkoutTemplate.Exercises.Add(exercise);
                var newExercise = exercise.GetDeepCopy();
                newExercise.WorkoutId = RealizedWorkout.Id;
                RealizedWorkout.Exercises.Add(newExercise);
            }
        }

        [RelayCommand]
        public async Task ExerciseIsDoneAsync(Exercise exercise)
        {
            exercise.IsDone = true;
        }
    }
}
