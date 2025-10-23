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

        public DbService(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Workout>().Wait();
            db.CreateTableAsync<Exercise>().Wait();
            db.CreateTableAsync<PerformedSet>().Wait();
            db.CreateTableAsync<ExerciseTemplate>().Wait();

            Debug.WriteLine($" SQLite database path: {dbPath}");
        }

        #region:WorkoutCRUD
        public Task<List<Workout>> GetWorkoutsAsync()
        {
            return db.Table<Workout>().ToListAsync();
        }
        public Task<int> CreateWorkoutAsync(Workout workout)
        {
            return db.InsertAsync(workout);
        }
        public Task<int> UpdateWorkoutAsync(Workout workout)
        {
            return db.UpdateAsync(workout);
        }
        public Task<int> DeleteWorkoutAsync(Workout workout)
        {
            return db.DeleteAsync(workout);
        }
        #endregion
        #region:ExerciseCRUD
        public Task<List<Exercise>> GetExercisesAsync()
        {
            return db.Table<Exercise>().ToListAsync();
        }
        public Task<List<Exercise>> GetExercisesByWorkoutIdAsync(int workoutId)
        {
            return db.Table<Exercise>().Where(e => e.WorkoutId == workoutId).ToListAsync();
        }
        public Task<int> CreateExerciseAsync(Exercise exercise)
        {
            return db.InsertAsync(exercise);
        }
        public Task<int> UpdateExerciseAsync(Exercise exercise)
        {
            return db.UpdateAsync(exercise);
        }
        public Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return db.DeleteAsync(exercise);
        }
        #endregion
        #region:PerformedSetCRUD
        public Task<List<PerformedSet>> GetPerformedSetsAsync()
        {
            return db.Table<PerformedSet>().ToListAsync();
        }
        public Task<List<PerformedSet>> GetPerformedSetsByExerciseIdAsync(int exerciseId)
        {
            return db.Table<PerformedSet>().Where(e => e.ExerciseId == exerciseId).ToListAsync();
        }
        public Task<int> CreatePerformedSetAsync(PerformedSet performedSet)
        {
            return db.InsertAsync(performedSet);
        }
        public Task<int> UpdatePerformedSetAsync(PerformedSet performedSet)
        {
            return db.UpdateAsync(performedSet);
        }
        public Task<int> DeletePerformedSetAsync(PerformedSet performedSet)
        {
            return db.DeleteAsync(performedSet);
        }
        #endregion
    }
}
