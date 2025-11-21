using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Models;
using Feleves_feladat.Services;

namespace Feleves_feladat
{
    [QueryProperty(nameof(WorkoutTemplateId), "workoutTemplateId")]
    public partial class WorkoutPageViewModel : ObservableObject
    {
        private readonly IDbService db;
        [ObservableProperty]
        private Workout workoutTemplate;
        [ObservableProperty]
        private int workoutTemplateId;
        [ObservableProperty]
        private Workout performedWorkout;

        private DateTime startTime;

        public bool DoesntHaveImageYet => PerformedWorkout is null || !PerformedWorkout.HasImage;
        public bool AllExercisesDone =>
            PerformedWorkout?.Exercises?.All(e => e.IsDone) == true;

        public WorkoutPageViewModel(IDbService db)
        {
            this.db = db;
        }
        
        [RelayCommand]
        public void ExerciseIsDone(Exercise exercise)
        {
            exercise.IsDone = true;
        }
        [RelayCommand]
        public async Task WorkoutCanceled()
        {
            await db.DeleteWorkoutAsync(PerformedWorkout);

            await Shell.Current.GoToAsync("//chooseworkout");
        }
        [RelayCommand]
        public async Task TakePictureAsync()
        {
            if (!MediaPicker.Default.IsCaptureSupported) return;

            FileResult? image = await MediaPicker.Default.CapturePhotoAsync();
            if(image != null)
            {
                string localURL = Path.Combine(FileSystem.Current.AppDataDirectory, image.FileName);
                if (!File.Exists(localURL))
                {
                    using Stream stream = await image.OpenReadAsync();
                    using FileStream fs = File.OpenWrite(localURL);
                    await stream.CopyToAsync(fs);
                }
                PerformedWorkout.ImageUrl = localURL;
                PerformedWorkout.HasImage = true;
                OnPropertyChanged(nameof(DoesntHaveImageYet));
            }
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

            await Shell.Current.GoToAsync("//chooseworkout");
            await Shell.Current.GoToAsync("//recentworkouts");
        }
        public async Task Initialize()
        {
            WorkoutTemplate = await db.GetWorkoutTemplateByIdAsync(WorkoutTemplateId);
            await InitializeExercisesFromDb();
        }
        private async Task InitializeExercisesFromDb()
        {
            PerformedWorkout = new()
            {
                Name = WorkoutTemplate.Name,
                Color = WorkoutTemplate.Color,
                IsTemplate = false,
                Date = DateTime.Today
            };
            await db.CreateWorkoutAsync(PerformedWorkout);
            startTime = DateTime.Now;

            var exercises = await db.GetExercisesByWorkoutIdAsync(WorkoutTemplate.Id);
            foreach (var exercise in exercises)
            {
                var newExercise = exercise.GetDeepCopy();
                newExercise.WorkoutId = PerformedWorkout.Id;
                newExercise.IsTemplate = false;
                newExercise.PropertyChanged += (_, e) =>
                {
                    if (e.PropertyName == nameof(Exercise.IsDone))
                        OnPropertyChanged(nameof(AllExercisesDone));
                };

                PerformedWorkout.Exercises.Add(newExercise);
            }
        }
        //private async Task OpenRecentWorkoutsAsync()
        //{
        //    await Shell.Current.GoToAsync("//recentworkouts");
        //}
    }
}
