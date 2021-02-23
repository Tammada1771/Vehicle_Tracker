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
    public enum ControlMode : int
    {
        Color = 0,
        Make = 1,
        Model = 2,
        Year = 3
    }

    public class Year
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Interaction logic for ucMaintainVehicle.xaml
    /// </summary>
    public partial class ucMaintainVehicle : UserControl
    {

        ControlMode controlmode;
        public List<BL.Models.Color> colors { get; set; }
        public List<Make> makes {get; set; }
        public List<Model> models {get; set; }
        List<Year> years;

        public ucMaintainVehicle(ControlMode controlmode, Guid id)
        {
            InitializeComponent();

            lblAttribute.Content = controlmode.ToString();
            this.controlmode = controlmode;

            Reload();

            cboAttribute.SelectedValue = id;
            cboAttribute.Tag = controlmode;
        }

        public ucMaintainVehicle(ControlMode controlmode, int id)
        {
            InitializeComponent();

            lblAttribute.Content = controlmode.ToString();
            this.controlmode = controlmode;

            Reload();

            cboAttribute.Text = id.ToString();
            cboAttribute.Tag = controlmode;
        }

        private async void Reload()
        {
            cboAttribute.ItemsSource = null;

            switch (controlmode)
            {
                case ControlMode.Color:
                    colors = (List<BL.Models.Color>)await ColorManager.Load();
                    cboAttribute.ItemsSource = colors;
                    break;
                case ControlMode.Make:
                    makes = (List<BL.Models.Make>)await MakeManager.Load();
                    cboAttribute.ItemsSource = makes;
                    break;
                case ControlMode.Model:
                    models = (List<BL.Models.Model>)await ModelManager.Load();
                    cboAttribute.ItemsSource = models;
                    break;
                case ControlMode.Year:
                    years = new List<Year>();
                    int id = 0;
                    for (int year = 2021; year > 1900; year--)
                    {
                        years.Add(new Year { Id = ++id, Description = year.ToString() });
                    }
                    cboAttribute.ItemsSource = years;
                    break;
                default:
                    break;
            }

            cboAttribute.DisplayMemberPath = "Description";
            cboAttribute.SelectedValuePath = "Id";
        }

        //public prorerty to expose the color make or model Id
        public Guid AttributeId
        {
            get { return (Guid)cboAttribute.SelectedValue; }
        }

        // public property to expose the Year
        public string AttributeText
        {
            get { return cboAttribute.Text; }
        }
    }
}
