using System.ComponentModel;
using VT.ViewModels;
using Xamarin.Forms;

namespace VT.Views
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