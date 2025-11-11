using Feleves_feladat.Models;
using System.Collections.ObjectModel;

namespace Feleves_feladat.Services
{
    public class WorkoutCreatorService
    {
        public ObservableCollection<Exercise> CurrentExercises { get; set; } = new();
    }
}
