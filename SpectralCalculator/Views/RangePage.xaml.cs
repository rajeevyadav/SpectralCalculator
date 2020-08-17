using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SpectralCalculator.Models;
using SpectralCalculator.Views;
using SpectralCalculator.ViewModels;

namespace SpectralCalculator.Views
{
    public partial class RangePage : ContentPage
    {
        RangeViewModel rvm;

        public RangePage()
        {
            InitializeComponent();
            rvm = (RangeViewModel)BindingContext;
        }

        // the user clicked in an Entry, so clear the field
        void entry_Focused(Object sender, FocusEventArgs e) => (sender as Entry).Text = "";

        // the user closed an Entry keyboard, so relay the value
        void entryLaserWavelength_Completed(Object sender, EventArgs e) => rvm.setLaserWavelength((sender as Entry).Text);
        void entryWavelengthStart_Completed(Object sender, EventArgs e) => rvm.setWavelengthStart((sender as Entry).Text);
        void entryWavenumberStart_Completed(Object sender, EventArgs e) => rvm.setWavenumberStart((sender as Entry).Text);
        void entryWavelengthEnd_Completed  (Object sender, EventArgs e) => rvm.setWavelengthEnd  ((sender as Entry).Text);
        void entryWavenumberEnd_Completed  (Object sender, EventArgs e) => rvm.setWavenumberEnd  ((sender as Entry).Text);
        void entryWavelengthRange_Completed(Object sender, EventArgs e) => rvm.setWavelengthRange((sender as Entry).Text);
        void entryWavenumberRange_Completed(Object sender, EventArgs e) => rvm.setWavenumberRange((sender as Entry).Text);
    }
}