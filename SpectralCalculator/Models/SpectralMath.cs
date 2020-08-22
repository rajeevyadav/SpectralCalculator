namespace SpectralCalculator.Models
{
    public class SpectralMath
    {
        const double NM_PER_CM = 1e7;

        public static double absoluteWavenumber(double wavelength) =>
            NM_PER_CM / wavelength;

        // actually returns "Raman shift in wavenumbers"
        public static double computeWavenumber(double laserWavelength, double wavelength) => 
            absoluteWavenumber(laserWavelength) - absoluteWavenumber(wavelength);

        // actually takes "Raman shift in wavenumbers"
        public static double computeWavelength(double laserWavelength, double wavenumber) =>
            1.0 / ((1.0/laserWavelength) - (wavenumber / 1e7));
    }
}
