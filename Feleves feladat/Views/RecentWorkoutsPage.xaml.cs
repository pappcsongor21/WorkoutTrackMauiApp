using CommunityToolkit.Mvvm.Messaging;
using Feleves_feladat.ViewModels;

namespace Feleves_feladat.Views;

public partial class RecentWorkoutsPage : ContentPage
{
    private readonly RecentWorkoutsPageViewModel vm;
    public RecentWorkoutsPage(RecentWorkoutsPageViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
        WeakReferenceMessenger.Default.Register<ErrorMessage>(this, async (s, message) =>
            {
                await Shell.Current.DisplayAlert(message.Title, message.Text, "OK");
            });
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await vm.LoadWorkoutsFromDbAsync();
    }
}