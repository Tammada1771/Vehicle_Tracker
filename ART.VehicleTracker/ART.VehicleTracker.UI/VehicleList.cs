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
        List<Vehicle> filteredVehicles;
        List<BL.Models.Color> colors;
        public VehicleList()

        {
            InitializeComponent();
        }

        private void BtnColors_Click(object sender, RoutedEventArgs e)
        {
            new MaintainColor().ShowDialog();
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

        private async void Rebind()
        {

            grdVehicles.ItemsSource = null;
            grdVehicles.ItemsSource = filteredVehicles;

            //0, 3, 4,5 
            grdVehicles.Columns[0].Visibility = Visibility.Hidden;
            grdVehicles.Columns[3].Visibility = Visibility.Hidden;
            grdVehicles.Columns[4].Visibility = Visibility.Hidden;
            grdVehicles.Columns[5].Visibility = Visibility.Hidden;

            // 6, 7, 8
            grdVehicles.Columns[6].Header = "Color";
            grdVehicles.Columns[7].Header = "Make";
            grdVehicles.Columns[8].Header = "Model";
        }

        private async void Reload()
        {
            vehicles = (List<Vehicle>)await VehicleManager.Load();
            filteredVehicles = vehicles;
            colors = (List<BL.Models.Color>)await ColorManager.Load();
            cboFilter.ItemsSource = null;
            cboFilter.ItemsSource = colors;
            cboFilter.DisplayMemberPath = "Description";
            cboFilter.SelectedValuePath = "Id";

            Rebind();

        }

        private void BtnNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            Vehicle vehicle = new Vehicle();
            MaintainVehicle maintainVehicle = new MaintainVehicle(vehicle);
            maintainVehicle.Owner = this;
            maintainVehicle.ShowDialog();

            vehicles.Add(vehicle);
            Rebind();
        }

        private void BtnEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            Vehicle vehicle = vehicles[grdVehicles.SelectedIndex];
            MaintainVehicle maintainVehicle = new MaintainVehicle(vehicle);
            maintainVehicle.Owner = this;
            maintainVehicle.ShowDialog();

            vehicles[grdVehicles.SelectedIndex] = vehicle;
            Rebind();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private void cboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboFilter.SelectedIndex > -1)
            {
                filteredVehicles = vehicles.Where(v => v.ColorId == colors[cboFilter.SelectedIndex].Id).ToList();
                Rebind();
            }
        }
    }
}
