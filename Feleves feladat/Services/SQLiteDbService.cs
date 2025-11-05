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
    public class SQLiteDbService : IDbService
    {
        private readonly SQLiteAsyncConnection db;

        SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;

        public SQLiteDbService(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath, Flags);
            db.CreateTableAsync<WorkoutTemplate>().Wait();
            db.CreateTableAsync<Workout>().Wait();
            db.CreateTableAsync<Exercise>().Wait();
            db.CreateTableAsync<PerformedSet>().Wait();
            db.CreateTableAsync<ExerciseTemplate>().Wait();

            GenerateSeedData();
            Debug.WriteLine($" SQLite database path: {dbPath}");
        }

        public async void GenerateSeedData()
        {
            WorkoutTemplate upperBodyCali = new() { Name = "Upper body cali", Color = "Purple"};
            await CreateWorkoutTemplateAsync(upperBodyCali);

            await CreateExerciseAsync(new Exercise() { Name = "banded pullup", Intensity = "35kg band", TargetReps = "5-8", TargetSets = 3, WorkoutId = upperBodyCali.Id });
            await CreateExerciseAsync(new Exercise() { Name = "ring dip hold", Intensity = "slightly assisted", TargetReps = "15s", TargetSets = 3, WorkoutId = upperBodyCali.Id });
            await CreateExerciseAsync(new Exercise() { Name = "inverted row", Intensity = "-1 step", TargetReps = "8-12", TargetSets = 3, WorkoutId = upperBodyCali.Id });
            await CreateExerciseAsync(new Exercise() { Name = "pushup", Intensity = "", TargetReps = "8-12", TargetSets = 3, WorkoutId = upperBodyCali.Id });
            await CreateExerciseAsync(new Exercise() { Name = "pike leg raise", Intensity = "fingertips at knee", TargetReps = "5-8", TargetSets = 3, WorkoutId = upperBodyCali.Id });

            WorkoutTemplate workout1 = new() { Name = "Lower body", Color = "Green"};
            await CreateWorkoutTemplateAsync(workout1);
            await CreateWorkoutTemplateAsync(new() { Name = "Upper body cali B", Color = "Purple" });
        }

        #region:WorkoutTemplateCRUD
        public async Task<List<WorkoutTemplate>> GetWorkoutTemplatesAsync()
        {
            return await db.Table<WorkoutTemplate>().ToListAsync();
        }
        public async Task<int> CreateWorkoutTemplateAsync(WorkoutTemplate workout)
        {
            return await db.InsertAsync(workout);
        }
        public async Task<int> UpdateWorkoutTemplateAsync(WorkoutTemplate workout)
        {
            return await db.UpdateAsync(workout);
        }
        public async Task<int> DeleteWorkoutTemplateAsync(WorkoutTemplate workout)
        {
            return await db.DeleteAsync(workout);
        }
        #endregion

        #region:WorkoutCRUD
        public async Task<List<Workout>> GetWorkoutsAsync()
        {
            return await db.Table<Workout>().ToListAsync();
        }
        public async Task<int> CreateWorkoutAsync(Workout workout)
        {
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
        public async Task<List<Exercise>> GetExercisesByWorkoutTemplateIdAsync(int workoutTemplateId)
        {
            return await db.Table<Exercise>().Where(e => e.WorkoutId == workoutTemplateId).ToListAsync();

        }
        public async Task<int> CreateExerciseAsync(Exercise exercise)
        {
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
