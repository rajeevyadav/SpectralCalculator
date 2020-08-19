using System;
using Xamarin.Forms;
using SpectralCalculator.ViewModels;

namespace SpectralCalculator.Views
{
    public partial class WidthPage : ContentPage
    {
        WidthViewModel wvm;

        public WidthPage()
        {
            InitializeComponent();
            wvm = (WidthViewModel)BindingContext;
        }

        // the user clicked "Done" on an Entry keyboard, so relay the value
        void entryLaserWavelength_Completed (Object sender, EventArgs e) => wvm.setLaserWavelength((sender as Entry).Text);
        void entryPeakWavelength_Completed  (Object sender, EventArgs e) => wvm.setPeakWavelength ((sender as Entry).Text);
        void entryPeakWavenumber_Completed  (Object sender, EventArgs e) => wvm.setPeakWavenumber ((sender as Entry).Text);
        void entryPeakWidthNM_Completed     (Object sender, EventArgs e) => wvm.setPeakWidthNM    ((sender as Entry).Text);
        void entryPeakWidthCM_Completed     (Object sender, EventArgs e) => wvm.setPeakWidthCM    ((sender as Entry).Text);
    }
}