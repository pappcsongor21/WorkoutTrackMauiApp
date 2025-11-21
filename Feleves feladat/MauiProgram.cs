using Feleves_feladat.Services;
using Feleves_feladat.ViewModels;
using Feleves_feladat.Views;
using Microsoft.Extensions.Logging;

namespace Feleves_feladat
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IDbService, SQLiteDbService>(s =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "workouts.db3");
                return new SQLiteDbService(dbPath);
            });


            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<ChooseWorkoutPageViewModel>();
            builder.Services.AddTransient<WorkoutPageViewModel>();
            builder.Services.AddTransient<RecentWorkoutsPageViewModel>();
            builder.Services.AddTransient<CreateWorkoutTemplateViewModel>();
            builder.Services.AddTransient<SelectExercisePageViewModel>();
            builder.Services.AddTransient<EditWorkoutTemplatePageViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<ChooseWorkoutPage>();
            builder.Services.AddTransient<WorkoutPage>();
            builder.Services.AddTransient<RecentWorkoutsPage>();
            builder.Services.AddTransient<WorkoutCreatorPage>();
            builder.Services.AddTransient<SelectExercisePage>();
            builder.Services.AddTransient<EditWorkoutTemplatePage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
