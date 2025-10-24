using Feleves_feladat.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.Services
{
    public class DbService : IDbService
    {
        private readonly SQLiteAsyncConnection db;
        private int globalWorkoutId = 1;
        private int globalExerciseId = 1;

        public DbService(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Workout>().Wait();
            db.CreateTableAsync<Exercise>().Wait();
            db.CreateTableAsync<PerformedSet>().Wait();
            db.CreateTableAsync<ExerciseTemplate>().Wait();

            InitializeDb();
            Debug.WriteLine($" SQLite database path: {dbPath}");
        }

        public async void InitializeDb()
        {
            Workout upperBodyCali = new() { Name = "Upper body cali", Color = "Purple" };
            await CreateWorkoutAsync(upperBodyCali);

            await CreateExerciseAsync(new Exercise() { Name = "banded pullup", Intensity = "35kg band", TargetReps = "5-8", TargetSets = 3, WorkoutId = upperBodyCali.Id });
            await CreateExerciseAsync(new Exercise() { Name = "ring dip hold", Intensity = "slightly assisted", TargetReps = "15s", TargetSets = 3, WorkoutId = upperBodyCali.Id });
            await CreateExerciseAsync(new Exercise() { Name = "inverted row", Intensity = "-1 step", TargetReps = "8-12", TargetSets = 3, WorkoutId = upperBodyCali.Id });
            await CreateExerciseAsync(new Exercise() { Name = "pushup", Intensity = "", TargetReps = "8-12", TargetSets = 3, WorkoutId = upperBodyCali.Id });

            Workout workout1 = new() { Name = "Lower body", Color = "Green" };
            await CreateWorkoutAsync(workout1);
            await CreateWorkoutAsync(new() { Name = "Upper body cali B", Color = "Purple" });
        }

        #region:WorkoutCRUD
        public async Task<List<Workout>> GetWorkoutsAsync()
        {
            return await db.Table<Workout>().ToListAsync();
        }
        public async Task<int> CreateWorkoutAsync(Workout workout)
        {
            workout.Id = globalWorkoutId++;
            return await db.InsertAsync(workout);
        }
        public async Task<int> UpdateWorkoutAsync(Workout workout)
        {
            return await db.UpdateAsync(workout);
        }
        public async Task<int> DeleteWorkoutAsync(Workout workout)
        {
            return await db.DeleteAsync(workout);
        }
        #endregion

        #region:ExerciseCRUD
        public async Task<List<Exercise>> GetExercisesAsync()
        {
            return await db.Table<Exercise>().ToListAsync();
        }
        public async Task<List<Exercise>> GetExercisesByWorkoutIdAsync(int workoutId)
        {
            return await db.Table<Exercise>().Where(e => e.WorkoutId == workoutId).ToListAsync();
        }
        public async Task<int> CreateExerciseAsync(Exercise exercise)
        {
            exercise.Id = globalExerciseId++;
            return await db.InsertAsync(exercise);
        }
        public async Task<int> UpdateExerciseAsync(Exercise exercise)
        {
            return await db.UpdateAsync(exercise);
        }
        public async Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return await db.DeleteAsync(exercise);
        }
        #endregion

        #region:PerformedSetCRUD
        public async Task<List<PerformedSet>> GetPerformedSetsAsync()
        {
            return await db.Table<PerformedSet>().ToListAsync();
        }
        public async Task<List<PerformedSet>> GetPerformedSetsByExerciseIdAsync(int exerciseId)
        {
            return await db.Table<PerformedSet>().Where(e => e.ExerciseId == exerciseId).ToListAsync();
        }
        public async Task<int> CreatePerformedSetAsync(PerformedSet performedSet)
        {
            return await db.InsertAsync(performedSet);
        }
        public async Task<int> UpdatePerformedSetAsync(PerformedSet performedSet)
        {
            return await db.UpdateAsync(performedSet);
        }
        public async Task<int> DeletePerformedSetAsync(PerformedSet performedSet)
        {
            return await db.DeleteAsync(performedSet);
        }
        #endregion
    }
}
