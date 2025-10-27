using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat
{
    [QueryProperty(nameof(Workout),"workout")]
    public partial class WorkoutPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private Workout workout;

        IDbService db;
        public WorkoutPageViewModel(IDbService db)
        {
            this.db = db;
        }
        public async Task InitializeExercisesFromDb()
        {
            var exercises = await db.GetExercisesByWorkoutIdAsync(Workout.Id);
            foreach(var exercise in exercises)
            {
                Workout.Exercises.Add(exercise);
            }
        }
    }
}
