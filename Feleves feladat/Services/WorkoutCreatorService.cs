using Feleves_feladat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.Services
{
    public class WorkoutCreatorService
    {
        public ObservableCollection<Exercise> CurrentExercises { get; set; } = new();
    }
}
