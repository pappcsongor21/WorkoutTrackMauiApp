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
        private Workout performedWorkout;

        private readonly IDbService db;

        public bool AllExercisesDone =>
            PerformedWorkout?.Exercises?.All(e => e.IsDone) == true;

        public WorkoutPageViewModel(IDbService db)
        {
            this.db = db;
        }
        public async Task InitializeExercisesFromDb()
        {
            WorkoutTemplate.Exercises.Clear();
            var exercises = await db.GetExercisesByWorkoutTemplateIdAsync(WorkoutTemplate.Id);
            PerformedWorkout = new()
            {
                Name = WorkoutTemplate.Name,
                Color = WorkoutTemplate.Color
            };
            foreach (var exercise in exercises)
            {
                WorkoutTemplate.Exercises.Add(exercise);
                var newExercise = exercise.GetDeepCopy();
                newExercise.WorkoutId = PerformedWorkout.Id;

                newExercise.PropertyChanged += (_, e) =>
                {
                    if (e.PropertyName == nameof(Exercise.IsDone))
                        OnPropertyChanged(nameof(AllExercisesDone));
                };

                PerformedWorkout.Exercises.Add(newExercise);
            }
        }

        [RelayCommand]
        public void ExerciseIsDone(Exercise exercise)
        {
            exercise.IsDone = true;
        }

        [RelayCommand]
        public async Task WorkoutFinishedAsync()
        {
            await db.CreateWorkoutAsync(PerformedWorkout);

            foreach(Exercise exercise in PerformedWorkout.Exercises)
            {
                await db.CreateExerciseAsync(exercise);
            }
        }
    }
}
