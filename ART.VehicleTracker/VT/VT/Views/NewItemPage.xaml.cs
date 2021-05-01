using System;
using System.Collections.Generic;
using System.ComponentModel;
using VT.Models;
using VT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VT.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}