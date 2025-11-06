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
    [QueryProperty(nameof(WorkoutTemplate), "workoutTemplate")]
    public partial class WorkoutPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private Workout workoutTemplate;

        [ObservableProperty]
        private Workout performedWorkout;
        private DateTime startTime;

        private readonly IDbService db;

        public bool AllExercisesDone =>
            PerformedWorkout?.Exercises?.All(e => e.IsDone) == true;

        public WorkoutPageViewModel(IDbService db)
        {
            this.db = db;
        }
        public async Task InitializeExercisesFromDb()
        {
            //WorkoutTemplate.Exercises.Clear();
            var exercises = await db.GetExercisesByWorkoutIdAsync(WorkoutTemplate.Id);
            PerformedWorkout = new()
            {
                Name = WorkoutTemplate.Name,
                Color = WorkoutTemplate.Color,
                IsTemplate = false,
                Date = DateTime.Today
            };
            await db.CreateWorkoutAsync(PerformedWorkout);
            startTime = DateTime.Now;
            foreach (var exercise in exercises)
            {
                //WorkoutTemplate.Exercises.Add(exercise);

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
            PerformedWorkout.Length = (DateTime.Now.Hour * 60 + DateTime.Now.Minute) - (startTime.Hour * 60 + startTime.Minute);
            await db.UpdateWorkoutAsync(PerformedWorkout);
            foreach (Exercise exercise in PerformedWorkout.Exercises)
            {
                await db.CreateExerciseAsync(exercise);
            }
        }
    }
}
