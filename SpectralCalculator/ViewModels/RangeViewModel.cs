using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using SpectralCalculator.Models;

namespace SpectralCalculator.ViewModels
{
    public class RangeViewModel : INotifyPropertyChanged
    {
        RangeModel rm = new RangeModel();

        public event PropertyChangedEventHandler PropertyChanged;

        public RangeViewModel()
        {
            openWebsite = new Command(async () => await Browser.OpenAsync("https://wasatchphotonics.com"));
        }

        public ICommand openWebsite { get; }

        public string Title
        {
            get => "Raman Range Calculator";
        }

        public double laserWavelength
        {
            get => rm.laserWavelength;
            private set
            {
                rm.laserWavelength = value;
                OnPropertyChanged();
            }
        }

        public double wavelengthStart
        {
            get => rm.wavelengthStart;
            private set
            {
                rm.wavelengthStart = value;
                OnPropertyChanged();
            }
        }

        public double wavenumberStart
        {
            get => rm.wavenumberStart;
            private set
            {
                rm.wavenumberStart = value;
                OnPropertyChanged();
            }
        }

        public double wavelengthEnd
        {
            get => rm.wavelengthEnd;
            private set
            {
                rm.wavelengthEnd = value;
                OnPropertyChanged();
            }
        }

        public double wavenumberEnd
        {
            get => rm.wavenumberEnd;
            private set
            {
                rm.wavenumberEnd = value;
                OnPropertyChanged();
            }
        }

        public double wavelengthRange
        {
            get => rm.wavelengthRange;
            private set
            {
                rm.wavelengthRange = value;
                OnPropertyChanged();
            }
        }

        public double wavenumberRange
        {
            get => rm.wavenumberRange;
            private set
            {
                rm.wavenumberRange = value;
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

                // leave the wavenumber column static, and recompute wavelengths
                // from wavenumbers
                computeWavelengthStart();
                computeWavelengthEnd();
                computeWavelengthRange();
            }
        }

        public void setWavelengthStart(string s)
        {
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                wavelengthStart = value;
                computeWavenumberStart();

                if (wavelengthEnd < wavelengthStart)
                {
                    wavelengthEnd = wavelengthStart;
                    computeWavenumberEnd();
                }

                computeRanges();
            }
        }

        public void setWavenumberStart(string s)
        {
            if (float.TryParse(s, out float value))
            {
                wavenumberStart = value;
                computeWavelengthStart();

                if (wavenumberEnd < wavenumberStart)
                {
                    wavenumberEnd = wavenumberStart;
                    computeWavelengthEnd();
                }    

                computeRanges();
            }
        }

        public void setWavelengthEnd(string s)
        {
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                wavelengthEnd = Math.Max(wavelengthStart, value);
                computeWavenumberEnd();
                computeRanges();
            }
        }

        public void setWavenumberEnd(string s)
        {
            if (float.TryParse(s, out float value))
            {
                wavenumberEnd = Math.Max(wavenumberStart, value);
                computeWavelengthEnd();
                computeRanges();
            }
        }

        public void setWavelengthRange(string s)
        {
            if (float.TryParse(s, out float value))
            {
                wavelengthEnd = wavelengthStart + value;
                computeWavenumberEnd();
                computeRanges();
            }
        }

        public void setWavenumberRange(string s)
        {
            if (float.TryParse(s, out float value))
            {
                wavenumberEnd = wavenumberStart + value;
                computeWavelengthEnd();
                computeRanges();
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // Computations
        ////////////////////////////////////////////////////////////////////////

        void computeWavelengthStart()
        {
            if (laserWavelength > 0)
                wavelengthStart = SpectralMath.computeWavelength(laserWavelength, wavenumberStart);
        }

        void computeWavelengthEnd()
        {
            if (laserWavelength > 0)
                wavelengthEnd = SpectralMath.computeWavelength(laserWavelength, wavenumberEnd);
        }

        void computeWavelengthRange()
        {
            wavelengthRange = wavelengthEnd - wavelengthStart;
        }

        void computeWavenumberStart()
        {
            if (laserWavelength > 0 && wavelengthStart > 0)
                wavenumberStart = SpectralMath.computeWavenumber(laserWavelength, wavelengthStart);
        }

        void computeWavenumberEnd()
        {
            if (laserWavelength > 0 && wavelengthEnd > 0)
                wavenumberEnd = SpectralMath.computeWavenumber(laserWavelength, wavelengthEnd);
        }

        void computeWavenumberRange()
        {
            wavenumberRange = wavenumberEnd - wavenumberStart;
        }

        void computeRanges()
        { 
            computeWavelengthRange();
            computeWavenumberRange();
        }

        ////////////////////////////////////////////////////////////////////////
        // Notifications
        ////////////////////////////////////////////////////////////////////////

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}