using System.ComponentModel;
using Xamarin.Forms;
using SpectralCalculator.ViewModels;

namespace SpectralCalculator.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}