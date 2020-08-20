using SpectralCalculator.Models;

namespace SpectralCalculator.ViewModels
{
    public class PeakViewModel : BaseViewModel
    {
        PeakModel pm = new PeakModel();

        public PeakViewModel() : base()
        {
        }

        ////////////////////////////////////////////////////////////////////////
        // Properties
        ////////////////////////////////////////////////////////////////////////

        // note we try to avoid issuing needless notifications and animations

        public string Title
        {
            get => "Raman Peak Calculator";
        }

        public double laserWavelength
        {
            get => pm.laserWavelength;
            private set
            {
                var notify = visiblyChanged(pm.laserWavelength, value);
                pm.laserWavelength = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double peakWavelength 
        {
            get => pm.peakWavelength;
            private set
            {
                var notify = visiblyChanged(pm.peakWavelength, value);
                pm.peakWavelength = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double peakWavenumber
        {
            get => pm.peakWavenumber;
            private set
            {
                var notify = visiblyChanged(pm.peakWavenumber, value);
                pm.peakWavenumber = value;
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

                pm.laserWavelength = value;
                computePeakWavelength();
            }
        }

        public void setPeakWavelength(string s)
        {
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                pm.peakWavelength = value;
                computePeakWavenumber();
            }
        }

        public void setPeakWavenumber(string s)
        {
            if (float.TryParse(s, out float value))
            {
                pm.peakWavenumber = value;
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

            peakWavenumber = SpectralMath.computeWavenumber(laserWavelength, peakWavelength);
        }

        void computePeakWavelength()
        {
            if (laserWavelength <= 0)
                return;

            peakWavelength = SpectralMath.computeWavelength(laserWavelength, peakWavenumber);
        }
    }
}