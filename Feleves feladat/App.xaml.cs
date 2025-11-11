using Feleves_feladat.Views;

namespace Feleves_feladat
{
    public partial class App : Application
    {
        public string DbPath = Path.Combine(FileSystem.AppDataDirectory, "workouts.db3");

        public App()
        {
            InitializeComponent();
            //Routing.RegisterRoute("workout", typeof(WorkoutPage));
            //Routing.RegisterRoute("chooseworkout", typeof(ChooseWorkoutPage));
            //Routing.RegisterRoute("recentworkouts", typeof(RecentWorkoutsPage));
            Routing.RegisterRoute("selectexercise", typeof(SelectExercisePage));
            Routing.RegisterRoute("workoutcreator", typeof(WorkoutCreatorPage));
            Routing.RegisterRoute("editworkouttemplate", typeof(EditWorkoutTemplatePage));


            // Fejlesztés alatt mindig töröljük az adatbázist
            if (File.Exists(DbPath))
                File.Delete(DbPath);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}