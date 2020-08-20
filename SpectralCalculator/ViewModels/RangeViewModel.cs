using System;
using SpectralCalculator.Models;

namespace SpectralCalculator.ViewModels
{
    public class RangeViewModel : BaseViewModel
    {
        RangeModel rm = new RangeModel();

        public RangeViewModel() : base()
        {
        }

        ////////////////////////////////////////////////////////////////////////
        // Properties
        ////////////////////////////////////////////////////////////////////////

        // note we try to avoid issuing needless notifications and animations

        public string Title
        {
            get => "Raman Range Calculator";
        }

        public double laserWavelength
        {
            get => rm.laserWavelength;
            private set
            {
                var notify = visiblyChanged(rm.laserWavelength, value);
                rm.laserWavelength = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double wavelengthStart
        {
            get => rm.wavelengthStart;
            private set
            {
                var notify = visiblyChanged(rm.wavelengthStart, value);
                rm.wavelengthStart = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double wavenumberStart
        {
            get => rm.wavenumberStart;
            private set
            {
                var notify = visiblyChanged(rm.wavenumberStart, value);
                rm.wavenumberStart = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double wavelengthEnd
        {
            get => rm.wavelengthEnd;
            private set
            {
                var notify = visiblyChanged(rm.wavelengthEnd, value);
                rm.wavelengthEnd = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double wavenumberEnd
        {
            get => rm.wavenumberEnd;
            private set
            {
                var notify = visiblyChanged(rm.wavenumberEnd, value);
                rm.wavenumberEnd = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double wavelengthRange
        {
            get => rm.wavelengthRange;
            private set
            {
                var notify = visiblyChanged(rm.wavelengthRange, value);
                rm.wavelengthRange = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double wavenumberRange
        {
            get => rm.wavenumberRange;
            private set
            {
                var notify = visiblyChanged(rm.wavelengthRange, value);
                rm.wavenumberRange = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // accept keyboard "completed" events from View code-behind
        ////////////////////////////////////////////////////////////////////////

        // note we don't call Property settors unless we want animation to fire

        public void setLaserWavelength(string s)
        {
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                rm.laserWavelength = value;

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

                rm.wavelengthStart = value;
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
                rm.wavenumberStart = value;
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

                rm.wavelengthEnd = value;
                if (wavelengthEnd < wavelengthStart)
                    wavelengthEnd = wavelengthStart;
                computeWavenumberEnd();
                computeRanges();
            }
        }

        public void setWavenumberEnd(string s)
        {
            if (float.TryParse(s, out float value))
            {
                rm.wavenumberEnd = value;
                if (wavenumberEnd < wavenumberStart)
                    wavenumberEnd = wavenumberStart;
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
    }
}