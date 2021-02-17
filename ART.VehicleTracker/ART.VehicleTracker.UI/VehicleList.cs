using ART.VehicleTracker.BL;
using ART.VehicleTracker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ART.VehicleTracker.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VehicleList : Window
    {
        List<Vehicle> vehicles;
        public VehicleList()
        {
            InitializeComponent();
        }

        private void BtnColors_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMakes_Click(object sender, RoutedEventArgs e)
        {
            new MaintainAttributes(ScreenMode.Make).ShowDialog();
        }

        private void BtnModels_Click(object sender, RoutedEventArgs e)
        {
            new MaintainAttributes(ScreenMode.Model).ShowDialog();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private async void Reload()
        {
            vehicles = (List<Vehicle>)await VehicleManager.Load();
            grdVehicles.ItemsSource = null;
            grdVehicles.ItemsSource = vehicles;


        }

        private void BtnNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            new MaintainVehicle().ShowDialog();
        }

        private void BtnEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            new MaintainVehicle().ShowDialog();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
