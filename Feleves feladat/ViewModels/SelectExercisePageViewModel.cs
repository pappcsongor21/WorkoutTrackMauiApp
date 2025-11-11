using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Models;
using Feleves_feladat.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.ViewModels
{
    public partial class SelectExercisePageViewModel
        (IDbService db, WorkoutCreatorService workoutCreatorService) : ObservableObject
    {
        private readonly IDbService db = db;
        private readonly WorkoutCreatorService workoutCreatorService = workoutCreatorService;

        public ObservableCollection<Exercise> Exercises { get; set; } = [];

        [RelayCommand]
        public async Task SelectExerciseAsync(Exercise selected)
        {
            workoutCreatorService.CurrentExercises.Add(selected);
            await Shell.Current.GoToAsync("..");
        }
        public async Task Init()
        {
            Exercises.Clear();
            var es = await db.GetExercisesAsync();
            foreach (var exercise in es)
            {
                Exercises.Add(exercise);
            }
        }
    }
}
