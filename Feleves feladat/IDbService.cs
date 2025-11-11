using Feleves_feladat.Models;

namespace Feleves_feladat
{
    public interface IDbService
    {
        Task<List<Workout>> GetWorkoutTemplatesAsync();
        Task<int> CreateWorkoutTemplateAsync(Workout workout);
        Task<int> UpdateWorkoutTemplateAsync(Workout workout);
        Task<int> DeleteWorkoutTemplateAsync(Workout workout);

        Task<List<Workout>> GetWorkoutsAsync();
        Task<int> CreateWorkoutAsync(Workout workout);
        Task<int> UpdateWorkoutAsync(Workout workout);
        Task<int> DeleteWorkoutAsync(Workout workout);

        Task<List<Exercise>> GetExercisesAsync();
        Task<List<Exercise>> GetExercisesByWorkoutIdAsync(int workoutId);
        Task<int> CreateExerciseAsync(Exercise exercise);
        Task<int> UpdateExerciseAsync(Exercise exercise);
        Task<int> DeleteExerciseAsync(Exercise exercise);

        Task<List<PerformedSet>> GetPerformedSetsAsync();
        Task<List<PerformedSet>> GetPerformedSetsByExerciseIdAsync(int exerciseId);
        Task<int> CreatePerformedSetAsync(PerformedSet performedSet);
        Task<int> UpdatePerformedSetAsync(PerformedSet performedSet);
        Task<int> DeletePerformedSetAsync(PerformedSet performedSet);

    }
}
