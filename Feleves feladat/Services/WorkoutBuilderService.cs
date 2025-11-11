using Feleves_feladat.Models;
using System.Collections.ObjectModel;

namespace Feleves_feladat.Services
{
    public class WorkoutBuilderService
    {
        public ObservableCollection<Exercise> CurrentExercises { get; set; } = [];
        public bool IsFirstOpenForEdit { get; set; } = true;
    }
}
