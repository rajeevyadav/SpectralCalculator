using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SpectralCalculator.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {
            openWebsite = new Command(async () => await Browser.OpenAsync("https://wasatchphotonics.com"));
        }

        public ICommand openWebsite { get; }

        protected bool visiblyChanged(double a, double b)
        {
            return string.Format($"{a:f3}") != string.Format($"{b:f3}");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
