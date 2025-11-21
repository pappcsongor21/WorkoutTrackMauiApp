using Feleves_feladat.Models;
using SQLite;
using System.Diagnostics;

namespace Feleves_feladat.Services
{
    public class SQLiteDbService : IDbService
    {
        private readonly SQLiteAsyncConnection db;

        readonly SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;

        public SQLiteDbService(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath, Flags);
            db.CreateTableAsync<Workout>().Wait();
            db.CreateTableAsync<Exercise>().Wait();
            db.CreateTableAsync<PerformedSet>().Wait();
            if(db.Table<Workout>().CountAsync().Result == 0)
                GenerateSeedData();
            Debug.WriteLine($" SQLite database path: {dbPath}");
        }

        public async void GenerateSeedData()
        {
            Workout upperBodyCali = new() { Name = "Upper body cali", Color = "Purple", IsTemplate = true };
            await CreateWorkoutTemplateAsync(upperBodyCali);

            await CreateExerciseAsync(new Exercise() { Name = "banded pullup", Intensity = "35kg band", TargetReps = "5-8", TargetSets = 3, WorkoutId = upperBodyCali.Id, IsTemplate = true });
            await CreateExerciseAsync(new Exercise() { Name = "ring dip hold", Intensity = "slightly assisted", TargetReps = "15s", TargetSets = 3, WorkoutId = upperBodyCali.Id, IsTemplate = true });
            await CreateExerciseAsync(new Exercise() { Name = "inverted row", Intensity = "-1 step", TargetReps = "8-12", TargetSets = 3, WorkoutId = upperBodyCali.Id, IsTemplate = true });
            await CreateExerciseAsync(new Exercise() { Name = "pushup", Intensity = "", TargetReps = "8-12", TargetSets = 3, WorkoutId = upperBodyCali.Id, IsTemplate = true });
            await CreateExerciseAsync(new Exercise() { Name = "pike leg raise", Intensity = "fingertips at knee", TargetReps = "5-8", TargetSets = 3, WorkoutId = upperBodyCali.Id, IsTemplate = true });

            Workout workout1 = new() { Name = "Lower body", Color = "Green", IsTemplate = true };
            await CreateWorkoutTemplateAsync(workout1);
            await CreateWorkoutTemplateAsync(new() { Name = "Upper body cali B", Color = "Purple", IsTemplate = true });
        }

        #region:WorkoutTemplateCRUD
        public async Task<List<Workout>> GetWorkoutTemplatesAsync()
        {
            var workouts = await db.Table<Workout>().ToListAsync();
            return workouts.FindAll(w => w.IsTemplate);
        }
        public async Task<Workout?> GetWorkoutTemplateByIdAsync(int id)
        {
            var workouts = await db.Table<Workout>().ToListAsync();
            return workouts.FirstOrDefault(w => w.Id == id && w.IsTemplate);
        }
        public async Task<int> CreateWorkoutTemplateAsync(Workout workout)
        {
            return await db.InsertAsync(workout);
        }
        public async Task<int> UpdateWorkoutTemplateAsync(Workout workout)
        {
            return await db.UpdateAsync(workout);
        }
        public async Task<int> DeleteWorkoutTemplateAsync(Workout workout)
        {
            return await db.DeleteAsync(workout);
        }
        #endregion

        #region:WorkoutCRUD
        public async Task<List<Workout>> GetWorkoutsAsync()
        {
            var workouts = await db.Table<Workout>().ToListAsync();
            return workouts.FindAll(w => !w.IsTemplate);
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

        #region:ExerciseTemplateCRUD
        public async Task<List<Exercise>> GetExerciseTemplatesAsync()
        {
            var exercises = await db.Table<Exercise>().ToListAsync();
            return exercises.FindAll(w => w.IsTemplate);
        }
        public async Task<List<Exercise>> GetExerciseTemplatesByWorkoutIdAsync(int workoutId)
        {
            return await db.Table<Exercise>().Where( e => e.WorkoutId == workoutId && e.IsTemplate).ToListAsync();
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
