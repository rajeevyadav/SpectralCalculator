using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;

namespace SpectralCalculator.ViewModels
{
    public class RangeViewModel : INotifyPropertyChanged
    {
        public RangeViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get => "Range";
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnAppearing()
        {
        }
    }
}