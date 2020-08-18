namespace SpectralCalculator.Models
{
    public class SpectralMath
    {
        public static double computeWavenumber(double laserWavelength, double wavelength) => 
            1e7 / laserWavelength - 1e7 / wavelength;

        public static double computeWavelength(double laserWavelength, double wavenumber) =>
            1.0 / ((1.0/laserWavelength) - (wavenumber * 1e-7));
    }
}
