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

        public RangeViewModel()
        {
            openWebsite = new Command(async () => await Browser.OpenAsync("https://wasatchphotonics.com"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
                Console.WriteLine($"RVM.laserWavelength = {value}");
                rm.laserWavelength = value;
                OnPropertyChanged();
            }
        }

        public double wavelengthStart
        {
            get => rm.wavelengthStart;
            private set
            {
                Console.WriteLine($"RVM.wavelengthStart = {value}");
                rm.wavelengthStart = value;
                OnPropertyChanged();
            }
        }

        public double wavenumberStart
        {
            get => rm.wavenumberStart;
            private set
            {
                Console.WriteLine($"RVM.wavenumberStart = {value}");
                rm.wavenumberStart = value;
                OnPropertyChanged();
            }
        }

        public double wavelengthEnd
        {
            get => rm.wavelengthEnd;
            private set
            {
                Console.WriteLine($"RVM.wavelengthEnd = {value}");
                rm.wavelengthEnd = value;
                OnPropertyChanged();
            }
        }

        public double wavenumberEnd
        {
            get => rm.wavenumberEnd;
            private set
            {
                Console.WriteLine($"RVM.wavenumberEnd = {value}");
                rm.wavenumberEnd = value;
                OnPropertyChanged();
            }
        }

        public double wavelengthRange
        {
            get => rm.wavelengthRange;
            private set
            {
                Console.WriteLine($"RVM.wavelengthRange = {value}");
                rm.wavelengthRange = value;
                OnPropertyChanged();
            }
        }

        public double wavenumberRange
        {
            get => rm.wavenumberRange;
            private set
            {
                Console.WriteLine($"RVM.wavenumberRange = {value}");
                rm.wavenumberRange = value;
                OnPropertyChanged();
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // accept keyboard "completed" events from View code-behind
        ////////////////////////////////////////////////////////////////////////

        public void setLaserWavelength(string s)
        {
            Console.WriteLine($"RVM.setLaserWavelength -> {s}");
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
            Console.WriteLine($"RVM.setWavelengthStart -> {s}");
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
            Console.WriteLine($"RVM.setWavenumberStart -> {s}");
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
            Console.WriteLine($"RVM.setWavelengthEnd -> {s}");
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
            Console.WriteLine($"RVM.setWavenumberEnd -> {s}");
            if (float.TryParse(s, out float value))
            {
                wavenumberEnd = Math.Max(wavenumberStart, value);
                computeWavelengthEnd();
                computeRanges();
            }
        }

        public void setWavelengthRange(string s)
        {
            Console.WriteLine($"RVM.setWavelengthRange -> {s}");
            if (float.TryParse(s, out float value))
            {
                wavelengthEnd = wavelengthStart + value;
                computeWavenumberEnd();
                computeRanges();
            }
        }

        public void setWavenumberRange(string s)
        {
            Console.WriteLine($"RVM.setWavenumberRange -> {s}");
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
            Console.WriteLine("computeWavenumberStart: start");
            if (laserWavelength > 0 && wavelengthStart > 0)
                wavenumberStart = SpectralMath.computeWavenumber(laserWavelength, wavelengthStart);
            Console.WriteLine("computeWavenumberStart: done");
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