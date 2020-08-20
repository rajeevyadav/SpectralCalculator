using System;
using SpectralCalculator.Models;

namespace SpectralCalculator.ViewModels
{
    public class WidthViewModel : BaseViewModel
    {
        WidthModel wm = new WidthModel();

        public WidthViewModel() : base()
        {
        }

        ////////////////////////////////////////////////////////////////////////
        // Properties
        ////////////////////////////////////////////////////////////////////////

        // note we try to avoid issuing needless notifications and animations

        public string Title
        {
            get => "Peak Width/Resolution Calculator";
        }

        public double laserWavelength
        {
            get => wm.laserWavelength;
            private set
            {
                var notify = visiblyChanged(wm.laserWavelength, value);
                wm.laserWavelength = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double peakWavelength
        {
            get => wm.peakWavelength;
            private set
            {
                var notify = visiblyChanged(wm.peakWavelength, value);
                wm.peakWavelength = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double peakWavenumber
        {
            get => wm.peakWavenumber;
            private set
            {
                var notify = visiblyChanged(wm.peakWavenumber, value);
                wm.peakWavenumber = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double peakWidthNM
        {
            get => wm.peakWidthNM;
            private set
            {
                var notify = visiblyChanged(wm.peakWidthNM, value);
                wm.peakWidthNM = value;
                if (notify)
                    OnPropertyChanged();
            }
        }

        public double peakWidthCM
        {
            get => wm.peakWidthCM;
            private set
            {
                var notify = visiblyChanged(wm.peakWidthCM, value);
                wm.peakWidthCM = value;
                if (notify)
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

                wm.laserWavelength = value;

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

                wm.peakWavelength = value;
                computePeakWavenumber();
                computeWidths();
            }
        }

        public void setPeakWavenumber(string s)
        {
            if (float.TryParse(s, out float value))
            {
                wm.peakWavenumber = value;
                computePeakWavelength();
                computeWidths();
            }
        }

        public void setPeakWidthNM(string s)
        {
            if (float.TryParse(s, out float value))
                computeWidthCM(wm.peakWidthNM = value);
        }

        public void setPeakWidthCM(string s)
        {
            if (float.TryParse(s, out float value))
                computeWidthNM(wm.peakWidthCM = value);
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
    }
}