using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using SpectralCalculator.Models;

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
            get => "Raman Range Calculator";
        }

        public double laserWavelength
        {
            get => _laserWavelength;
            private set
            {
                Console.WriteLine($"RVM.laserWavelength = {value}");
                _laserWavelength = value;
                OnPropertyChanged();
            }
        }
        double _laserWavelength;

        public double wavelengthStart
        {
            get => _wavelengthStart;
            private set
            {
                Console.WriteLine($"RVM.wavelengthStart = {value}");
                _wavelengthStart = value;
                OnPropertyChanged();
            }
        }
        double _wavelengthStart;

        public double wavenumberStart
        {
            get => _wavenumberStart;
            private set
            {
                Console.WriteLine($"RVM.wavenumberStart = {value}");
                _wavenumberStart = value;
                OnPropertyChanged();
            }
        }
        double _wavenumberStart;

        public double wavelengthEnd
        {
            get => _wavelengthEnd;
            private set
            {
                Console.WriteLine($"RVM.wavelengthEnd = {value}");
                _wavelengthEnd = value;
                OnPropertyChanged();
            }
        }
        double _wavelengthEnd;

        public double wavenumberEnd
        {
            get => _wavenumberEnd;
            private set
            {
                Console.WriteLine($"RVM.wavenumberEnd = {value}");
                _wavenumberEnd = value;
                OnPropertyChanged();
            }
        }
        double _wavenumberEnd;

        public double wavelengthRange
        {
            get => _wavelengthRange;
            private set
            {
                Console.WriteLine($"RVM.wavelengthRange = {value}");
                _wavelengthRange = value;
                OnPropertyChanged();
            }
        }
        double _wavelengthRange;

        public double wavenumberRange
        {
            get => _wavenumberRange;
            private set
            {
                Console.WriteLine($"RVM.wavenumberRange = {value}");
                _wavenumberRange = value;
                OnPropertyChanged();
            }
        }
        double _wavenumberRange;

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

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            Console.WriteLine($"RVM.OnPropertyChanged({propertyName})");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}