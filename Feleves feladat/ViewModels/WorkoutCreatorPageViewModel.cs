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
    public partial class WorkoutCreatorPageViewModel(WorkoutCreatorService workoutBuilderService) : ObservableObject
    {
        private readonly WorkoutCreatorService workoutBuilderService = workoutBuilderService;
        public ObservableCollection<Exercise> Exercises => workoutBuilderService.CurrentExercises;

        [RelayCommand]
        public async Task GoToSelectExerciseAsync()
        {
            await Shell.Current.GoToAsync("selectexercise");
        }
    }
}
