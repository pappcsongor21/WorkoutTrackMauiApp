namespace Feleves_feladat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute("workout", typeof(WorkoutPage));
            Routing.RegisterRoute("chooseworkout", typeof(ChooseWorkoutPage));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}