using System;
using System.Collections.Generic;
using SpectralCalculator.ViewModels;
using SpectralCalculator.Views;
using Xamarin.Forms;

namespace SpectralCalculator
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            // Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
