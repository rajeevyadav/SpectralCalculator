using System;
using Xamarin.Forms;
using SpectralCalculator.ViewModels;

namespace SpectralCalculator.Views
{
    public partial class RangePage : ContentPage
    {
        RangeViewModel rvm;
        AnimatedEntries animatedEntries = new AnimatedEntries();

        public RangePage()
        {
            InitializeComponent();
            rvm = (RangeViewModel)BindingContext;
            rvm.PropertyChanged += animatedEntries.onPropertyChanged;
            animatedEntries.add("laserWavelength", laserWavelength);
            animatedEntries.add("wavelengthStart", wavelengthStart);
            animatedEntries.add("wavenumberStart", wavenumberStart);
            animatedEntries.add("wavelengthEnd",   wavelengthEnd);
            animatedEntries.add("wavenumberEnd",   wavenumberEnd);
            animatedEntries.add("wavelengthRange", wavelengthRange);
            animatedEntries.add("wavenumberRange", wavenumberRange);
        }

        // the user clicked "Done" on an Entry keyboard, so relay the value
        void entryLaserWavelength_Completed(Object sender, EventArgs e) => rvm.setLaserWavelength((sender as Entry).Text);
        void entryWavelengthStart_Completed(Object sender, EventArgs e) => rvm.setWavelengthStart((sender as Entry).Text);
        void entryWavenumberStart_Completed(Object sender, EventArgs e) => rvm.setWavenumberStart((sender as Entry).Text);
        void entryWavelengthEnd_Completed  (Object sender, EventArgs e) => rvm.setWavelengthEnd  ((sender as Entry).Text);
        void entryWavenumberEnd_Completed  (Object sender, EventArgs e) => rvm.setWavenumberEnd  ((sender as Entry).Text);
        void entryWavelengthRange_Completed(Object sender, EventArgs e) => rvm.setWavelengthRange((sender as Entry).Text);
        void entryWavenumberRange_Completed(Object sender, EventArgs e) => rvm.setWavenumberRange((sender as Entry).Text);
    }
}