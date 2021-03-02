using ART.VehicleTracker.BL;
using ART.VehicleTracker.BL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private readonly ILogger<VehicleList> logger;

        Vehicle vehicle;

        public VehicleList(ILogger<VehicleList> _logger)
        {
            logger = _logger;
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

            Style headerStyle = new Style();
            DataGridColumnHeader header = new DataGridColumnHeader();
            headerStyle.TargetType = header.GetType();

            //Setter setter = new Setter();
            //setter.Property = FontSizeProperty;
            //setter.Value = 10.0;
            //headerStyle.Setters.Add(setter);

            headerStyle.Setters.Add(new Setter { Property = Control.BackgroundProperty, Value = Brushes.LightYellow });
            headerStyle.Setters.Add(new Setter { Property = Control.FontFamilyProperty, Value = new FontFamily("Verdana") });
            headerStyle.Setters.Add(new Setter { Property = Control.FontWeightProperty, Value = FontWeights.Bold });
            headerStyle.Setters.Add(new Setter { Property = Control.FontStyleProperty, Value =FontStyles.Italic });
            headerStyle.Setters.Add(new Setter { Property = Control.BorderThicknessProperty, Value = new Thickness(1) });
            headerStyle.Setters.Add(new Setter { Property = Control.BorderBrushProperty, Value = Brushes.Black });
            headerStyle.Setters.Add(new Setter { Property = Control.HorizontalContentAlignmentProperty, Value = HorizontalAlignment.Center });

            grdVehicles.Columns[4].HeaderStyle = headerStyle;
            grdVehicles.Columns[5].HeaderStyle = headerStyle;
            grdVehicles.Columns[6].HeaderStyle = headerStyle;
            grdVehicles.Columns[7].HeaderStyle = headerStyle;
            grdVehicles.Columns[8].HeaderStyle = headerStyle;

            Setter setterRow = new Setter();
            setterRow.Property = FontSizeProperty;
            setterRow.Value = 18.0;
            setterRow.Property = Control.ForegroundProperty;
            setterRow.Value = Brushes.Blue;
            setterRow.Property = Control.BackgroundProperty;
            setterRow.Value = Brushes.Pink;
           // grdVehicles.RowStyle.Setters.Add(setterRow);
        }

        private async void Reload()
        {
            vehicles = (List<Vehicle>)await VehicleManager.Load();
            filteredVehicles = vehicles;

            logger.LogInformation("Loaded " + vehicles.Count + " Vehicles.");

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
            try
            {
                Vehicle vehicle = vehicles[grdVehicles.SelectedIndex];
                MaintainVehicle maintainVehicle = new MaintainVehicle(vehicle);
                maintainVehicle.Owner = this;
                maintainVehicle.ShowDialog();

                vehicles[grdVehicles.SelectedIndex] = vehicle;
                Rebind();
                throw new Exception("Try to load vehicles");
            }
            catch (Exception ex)
            {

                logger.LogError("Error editing Vehicle: " + ex.Message);
            }
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
