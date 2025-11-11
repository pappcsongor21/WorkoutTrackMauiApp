using CommunityToolkit.Mvvm.ComponentModel;

namespace Feleves_feladat
{
    public partial class Goal : ObservableObject
    {
        [ObservableProperty]
        private string name;
    }
}
