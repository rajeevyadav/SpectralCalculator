using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SpectralCalculator.ViewModels;

namespace SpectralCalculator.Views
{
    public partial class PeakPage : ContentPage
    {
        PeakViewModel pvm;

        public PeakPage()
        {
            InitializeComponent();
            pvm = (PeakViewModel)BindingContext;
        }

        // the user clicked in an Entry, so clear the field
        void entry_Focused(Object sender, FocusEventArgs e) => (sender as Entry).Text = "";

        // the user closed an Entry keyboard, so relay the value
        void entryLaserWavelength_Completed(Object sender, EventArgs e) => pvm.setLaserWavelength((sender as Entry).Text);
        void entryPeakWavelength_Completed(Object sender, EventArgs e) => pvm.setPeakWavelength((sender as Entry).Text);
        void entryPeakWavenumber_Completed(Object sender, EventArgs e) => pvm.setPeakWavenumber((sender as Entry).Text);
    }
}