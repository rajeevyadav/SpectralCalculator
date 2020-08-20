using System;
using Xamarin.Forms;
using SpectralCalculator.ViewModels;

namespace SpectralCalculator.Views
{
    public partial class PeakPage : ContentPage
    {
        PeakViewModel pvm;
        AnimatedEntries animatedEntries = new AnimatedEntries();

        public PeakPage()
        {
            InitializeComponent();
            pvm = (PeakViewModel)BindingContext;
            pvm.PropertyChanged += animatedEntries.onPropertyChanged;
            animatedEntries.add("laserWavelength", laserWavelength);
            animatedEntries.add("peakWavelength", peakWavelength);
            animatedEntries.add("peakWavenumber", peakWavenumber);
        }

        // the user clicked "Done" on an Entry keyboard, so relay the value
        void entryLaserWavelength_Completed(Object sender, EventArgs e) => pvm.setLaserWavelength((sender as Entry).Text);
        void entryPeakWavelength_Completed (Object sender, EventArgs e) => pvm.setPeakWavelength ((sender as Entry).Text);
        void entryPeakWavenumber_Completed (Object sender, EventArgs e) => pvm.setPeakWavenumber ((sender as Entry).Text);
    }
}