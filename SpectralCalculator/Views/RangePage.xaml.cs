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
        public RangePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // _viewModel.OnAppearing();
        }
    }
}