using Feleves_feladat.Views;

namespace Feleves_feladat
{
    public partial class App : Application
    {
        public string DbPath = Path.Combine(FileSystem.AppDataDirectory, "workouts.db3");


        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute("workout", typeof(WorkoutPage));
            //Routing.RegisterRoute("chooseworkout", typeof(ChooseWorkoutPage));
            //Routing.RegisterRoute("recentworkouts", typeof(RecentWorkoutsPage));
            Routing.RegisterRoute("selectexercise", typeof(SelectExercisePage));
            Routing.RegisterRoute("workoutcreator", typeof(WorkoutCreatorPage));
            Routing.RegisterRoute("editworkouttemplate", typeof(EditWorkoutTemplatePage));

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("UNHANDLED EX: " + e.ExceptionObject);
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("TASK ERROR: " + e.Exception);
            };

            //#if DEBUG
            //if (File.Exists(DbPath))
            //    File.Delete(DbPath);
            //#endif
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}