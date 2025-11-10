using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.ViewModels
{
    public partial class RecentWorkoutsViewModel(IDbService db) : ObservableObject
    {
        private readonly IDbService db = db;
        public ObservableCollection<Workout> Workouts { get; set; } = [];
        public async Task LoadWorkoutsFromDbAsync()
        {
            Workouts.Clear();
            var ws = await db.GetWorkoutsAsync();
            foreach(var workout in ws)
                Workouts.Add(workout);
        }

        
    }
}
