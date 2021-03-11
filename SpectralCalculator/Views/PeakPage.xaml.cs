using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Globalization;
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
            //initFormatSwitchToggled();
            pvm = (PeakViewModel)BindingContext;
            pvm.PropertyChanged += animatedEntries.onPropertyChanged;
            animatedEntries.add("laserWavelength", laserWavelength);
            animatedEntries.add("peakWavelength", peakWavelength);
            animatedEntries.add("peakWavenumber", peakWavenumber);
        }

        /* Handled startup by using preferences to load true meaning 0.000,00 format and false for 0,000.00 format
         * public void initFormatSwitchToggled()
        {
            string currentCulture = Preferences.Get("numberForm", "false");
            if (currentCulture == "true")
            {
                numberSwitch.IsToggled = true;
            }
            else
            {
                numberSwitch.IsToggled = false;
            }
        }*/

        // the user clicked "Done" on an Entry keyboard, so relay the value
        void entryLaserWavelength_Completed(Object sender, EventArgs e) => pvm.setLaserWavelength((sender as Entry).Text);
        void entryPeakWavelength_Completed (Object sender, EventArgs e) => pvm.setPeakWavelength ((sender as Entry).Text);
        void entryPeakWavenumber_Completed (Object sender, EventArgs e) => pvm.setPeakWavenumber ((sender as Entry).Text);
    }
}