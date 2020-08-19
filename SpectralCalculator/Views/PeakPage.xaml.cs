using System;
using Xamarin.Forms;
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

        // the user clicked "Done" on an Entry keyboard, so relay the value
        void entryLaserWavelength_Completed(Object sender, EventArgs e) => pvm.setLaserWavelength((sender as Entry).Text);
        void entryPeakWavelength_Completed (Object sender, EventArgs e) => pvm.setPeakWavelength ((sender as Entry).Text);
        void entryPeakWavenumber_Completed (Object sender, EventArgs e) => pvm.setPeakWavenumber ((sender as Entry).Text);

        void EntryDone_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
        }
    }
}