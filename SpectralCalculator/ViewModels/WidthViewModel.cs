using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using SpectralCalculator.Models;

namespace SpectralCalculator.ViewModels
{
    public class WidthViewModel : INotifyPropertyChanged
    {
        WidthModel wm = new WidthModel();

        public event PropertyChangedEventHandler PropertyChanged;

        public WidthViewModel()
        {
            openWebsite = new Command(async () => await Browser.OpenAsync("https://wasatchphotonics.com"));
        }

        public ICommand openWebsite { get; }

        public string Title
        {
            get => "Peak Width/Resolution Calculator";
        }

        public double laserWavelength
        {
            get => wm.laserWavelength;
            private set
            {
                wm.laserWavelength = value;
                OnPropertyChanged();
            }
        }

        public double peakWavelength
        {
            get => wm.peakWavelength;
            private set
            {
                wm.peakWavelength = value;
                OnPropertyChanged();
            }
        }

        public double peakWavenumber
        {
            get => wm.peakWavenumber;
            private set
            {
                wm.peakWavenumber= value;
                OnPropertyChanged();
            }
        }

        public double peakWidthNM
        {
            get => wm.peakWidthNM;
            private set
            {
                wm.peakWidthNM = value;
                OnPropertyChanged();
            }
        }

        public double peakWidthCM
        {
            get => wm.peakWidthCM;
            private set
            {
                wm.peakWidthCM = value;
                OnPropertyChanged();
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // accept keyboard "completed" events from View code-behind
        ////////////////////////////////////////////////////////////////////////

        public void setLaserWavelength(string s)
        {
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                laserWavelength = value;

                if (peakWavenumber != 0)
                    computePeakWavelength();
                else
                    computePeakWavenumber();

                computeWidths();
            }
        }

        public void setPeakWavelength(string s)
        {
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                peakWavelength = value;
                computePeakWavenumber();
                computeWidths();
            }
        }

        public void setPeakWavenumber(string s)
        {
            if (float.TryParse(s, out float value))
            {
                peakWavenumber = value;
                computePeakWavelength();
                computeWidths();
            }
        }

        public void setPeakWidthNM(string s)
        {
            if (float.TryParse(s, out float value))
                computeWidthCM(peakWidthNM = value);
        }

        public void setPeakWidthCM(string s)
        {
            if (float.TryParse(s, out float value))
                computeWidthNM(peakWidthCM = value);
        }

        ////////////////////////////////////////////////////////////////////////
        // Computations
        ////////////////////////////////////////////////////////////////////////

        void computePeakWavelength()
        {
            if (laserWavelength > 0)
                peakWavelength = SpectralMath.computeWavelength(laserWavelength, peakWavenumber);
        }

        void computePeakWavenumber()
        {
            if (laserWavelength > 0)
                peakWavenumber = SpectralMath.computeWavenumber(laserWavelength, peakWavelength);
        }

        void computeWidthNM(double widthCM)
        {
            double lftFootCM = wm.peakWavenumber - widthCM / 2.0;
            double rgtFootCM = wm.peakWavenumber + widthCM / 2.0;

            double lftFootNM = SpectralMath.computeWavelength(laserWavelength, lftFootCM);
            double rgtFootNM = SpectralMath.computeWavelength(laserWavelength, rgtFootCM);

            peakWidthNM = rgtFootNM - lftFootNM;
        }

        void computeWidthCM(double widthNM)
        {
            double lftFootNM = wm.peakWavelength - widthNM / 2.0;
            double rgtFootNM = wm.peakWavelength + widthNM / 2.0;

            double lftFootCM = SpectralMath.computeWavenumber(laserWavelength, lftFootNM);
            double rgtFootCM = SpectralMath.computeWavenumber(laserWavelength, rgtFootNM);

            peakWidthCM = rgtFootCM - lftFootCM;
        }

        void computeWidths()
        { 
            if (peakWidthCM > 0)
                computeWidthNM(peakWidthCM);
            else if (peakWidthNM > 0)
                computeWidthCM(peakWidthNM);
        }

        ////////////////////////////////////////////////////////////////////////
        // Notifications
        ////////////////////////////////////////////////////////////////////////

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}