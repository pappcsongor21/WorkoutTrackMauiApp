using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat
{
    public partial class MainPageViewModel: ObservableObject
    {
        private readonly IDbService db;
        public ObservableCollection<Goal> Goals { get; set; }

        public MainPageViewModel(IDbService db)
        {
            Goals = new ObservableCollection<Goal>();
            Goals.Add(new Goal() { Name = "10 pullups" });
            Goals.Add(new Goal() { Name = "7 clean dips" });
            Goals.Add(new Goal() { Name = "3x4 pistol squats" });
            this.db = db;


        }

        [RelayCommand]
        public async Task ShowWorkoutsAsync()
        {
            await Shell.Current.GoToAsync("chooseworkout");
        }
    }
}
