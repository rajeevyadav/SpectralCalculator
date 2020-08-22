using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using SpectralCalculator.Models;

namespace SpectralCalculator.ViewModels
{
    public class PeakViewModel : BaseViewModel
    {
        PeakModel pm = new PeakModel();

        public PeakViewModel() : base()
        {
            sourceCommand = new Command(async () => await Browser.OpenAsync("https://github.com/WasatchPhotonics/SpectralCalculator"));
        }

        public ICommand sourceCommand { get; }

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
                OnPropertyChanged(nameof(explanation));
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
                OnPropertyChanged(nameof(explanation));
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
                OnPropertyChanged(nameof(explanation));
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

        ////////////////////////////////////////////////////////////////////////
        // Explanation
        ////////////////////////////////////////////////////////////////////////

        public bool explainThis
        {
            get => _explainThis;
            set
            {
                _explainThis = value;
                OnPropertyChanged();
            }
        }
        bool _explainThis;

        public string explanation
        {
            get
            {
                var laserAbsCM = SpectralMath.absoluteWavenumber(pm.laserWavelength);
                var peakAbsCM = SpectralMath.absoluteWavenumber(pm.peakWavelength);

                return string.Format(
                    "<div style=\"font-family: sans-serif; font-size: x-large\">" +
                    "<p>Every wavelength (λ) has an <b>absolute wavenumber</b>, expressed in " +
                    "inverse centimeters (1/cm, or cm⁻¹).  This is literally the number " +
                    "of <i>waves</i> of the given <i>wave length</i> which, if laid end-to-end, would " +
                    "\"fit\" in one centimeter.  For instance, if a photon had a wavelength " +
                    "of 1000nm (1µm), its absolute wavenumber would be 10,000, because " +
                    "10,000 × 1µm = 1cm.</p>" +

                   $"<p>The <i>absolute wavenumber</i> of a {pm.laserWavelength:f2}nm laser " +
                   $"is {laserAbsCM:f2}cm⁻¹.  Similarly, the <i>absolute wavenumber</i> of " +
                   $"the peak wavelength {pm.peakWavelength:f2}nm is {peakAbsCM:f2}cm⁻¹. Therefore, " +
                    "The <b>Raman shift in wavenumbers</b> (ṽ) for that peak, at that excitation, " +
                   $"would be {laserAbsCM:f2} - {peakAbsCM:f2} = {pm.peakWavenumber:f2}cm⁻¹.</p>" +
                    "</div>"
                );
            }
        }
    }
}