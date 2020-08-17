using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using SpectralCalculator.Models;

namespace SpectralCalculator.ViewModels
{
    public class PeakViewModel : INotifyPropertyChanged
    {
        public PeakViewModel()
        {
            openWebsite = new Command(async () => await Browser.OpenAsync("https://wasatchphotonics.com"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand openWebsite { get; }

        public string Title
        {
            get => "Raman Peak Calculator";
        }

        public double laserWavelength
        {
            get => _laserWavelength;
            private set
            {
                Console.WriteLine($"PVM.laserWavelength = {value}");
                _laserWavelength = value;
                OnPropertyChanged();
            }
        }
        double _laserWavelength;

        public double peakWavelength
        {
            get => _peakWavelength;
            private set
            {
                Console.WriteLine($"PVM.peakWavelength = {value}");
                _peakWavelength = value;
                OnPropertyChanged();
            }
        }
        double _peakWavelength;

        public double peakWavenumber
        {
            get => _peakWavenumber;
            private set
            {
                Console.WriteLine($"PVM.peakWavenumber = {value}");
                _peakWavenumber = value;
                OnPropertyChanged();
            }
        }
        double _peakWavenumber;

        ////////////////////////////////////////////////////////////////////////
        // accept keyboard "completed" events from View code-behind
        ////////////////////////////////////////////////////////////////////////

        public void setLaserWavelength(string s)
        {
            Console.WriteLine($"PVM.setLaserWavelength -> {s}");
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                laserWavelength = value;

                if (peakWavelength > 0)
                    computePeakWavenumber();
                else
                    computePeakWavelength();
            }
        }

        public void setPeakWavelength(string s)
        {
            Console.WriteLine($"PVM.setPeakWavelength -> {s}");
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                peakWavelength = value;
                computePeakWavenumber();
            }
        }

        public void setPeakWavenumber(string s)
        {
            Console.WriteLine($"PVM.setPeakWavenumber -> {s}");
            if (float.TryParse(s, out float value))
            {
                peakWavenumber = value;
                computePeakWavelength();
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // Computations
        ////////////////////////////////////////////////////////////////////////

        void computePeakWavenumber()
        {
            if (laserWavelength <= 0)
                return;

            var result = SpectralMath.computeWavenumber(laserWavelength, peakWavelength);
            Console.WriteLine($"PVM.computePeakWavenumber -> {result}");
            peakWavenumber = result;
        }

        void computePeakWavelength()
        {
            if (laserWavelength <= 0)
                return;

            var result = SpectralMath.computeWavelength(laserWavelength, peakWavenumber);
            Console.WriteLine($"PVM.computePeakWavelength -> {result}");
            peakWavelength = result;
        }

        ////////////////////////////////////////////////////////////////////////
        // Notifications
        ////////////////////////////////////////////////////////////////////////

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}