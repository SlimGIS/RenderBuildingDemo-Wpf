using SlimGis.MapKit.Controls;
using SlimGis.MapKit.Geometries;
using SlimGis.MapKit.Layers;
using SlimGis.MapKit.Symbologies;
using SlimGis.MapKit.Wpf;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AltitudeFillDemo_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LayerOverlay baseOverlay;
        private LayerOverlay buildingOverlay;
        private AltitudeFillStyle altitudeFillStyle;

        private readonly int[] altitudeArray = new[] { 20, 30, 40, 50 };
        private readonly int[] directionArray = new[] { 0, 45, 90, 135, 180, 225, 270, 315 };

        public MainWindow()
        {
            InitializeComponent();
            altitudeComboBox.ItemsSource = altitudeArray;
            directionComboBox.ItemsSource = directionArray;
        }

        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            mapControl.MapUnit = GeoUnit.Meter;

            baseOverlay = new LayerOverlay();
            CssDocument doc = CssDocument.Load(@"SampleData/Default.sgcss");
            baseOverlay.LoadStyledLayers("SampleData", doc);
            mapControl.Overlays.Add(baseOverlay);

            buildingOverlay = new LayerOverlay();
            buildingOverlay.TileMode = TileMode.SingleTile;
            altitudeFillStyle = new AltitudeFillStyle(GeoColor.FromHtml("#E6E1DF"), GeoColor.FromHtml("#80D3CDCA"), 1);
            altitudeFillStyle.AltitudeUnit = LengthUnit.Meter;

            ShapefileLayer buildingsLayer = new ShapefileLayer(@"SampleData/Buildings.shp");
            buildingsLayer.Styles.Add(altitudeFillStyle);
            buildingOverlay.Layers.Add(buildingsLayer);

            ShapefileLayer stadiumLayer = new ShapefileLayer(@"SampleData/Stadium.shp");
            stadiumLayer.Styles.Add(altitudeFillStyle);
            buildingOverlay.Layers.Add(stadiumLayer);

            mapControl.Overlays.Add(buildingOverlay);
            mapControl.CurrentBound = new GeoBound(11586390.5663, 3584893.2152, 11586849.1885, 3585104.8502);
            mapControl.Refresh();

            directionComboBox.SelectedIndex = 2;
            altitudeComboBox.SelectedIndex = 0;
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (altitudeComboBox.SelectedValue == null || directionComboBox.SelectedValue == null) return;

            directionComboBox.IsEnabled = false;
            altitudeComboBox.IsEnabled = false;
            int directionValue = (int)directionComboBox.SelectedValue;
            int altitudeValue = (int)altitudeComboBox.SelectedValue;


            altitudeFillStyle.AltitudeDirection = directionValue;
            if (altitudeValue != 0)
            {
                float refreshCount = 20;
                altitudeFillStyle.Altitude = 0;
                float step = altitudeValue / refreshCount;
                for (int i = 0; i <= refreshCount; i++)
                {
                    await Task.Factory.StartNew(() =>
                    {
                        altitudeFillStyle.Altitude += step;
                    });
                    buildingOverlay.Refresh();
                    await Task.Run(() => Thread.Sleep(16));
                }
            }
            directionComboBox.IsEnabled = true;
            altitudeComboBox.IsEnabled = true;
        }
    }
}
