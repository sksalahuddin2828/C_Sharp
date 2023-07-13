// you'll need to add references to the GMap.NET.Core and GMap.NET.WindowsForms libraries. 
// You can download the GMap.NET library from its official GitHub repository (https://github.com/judero01col/GMap.NET) or install it using NuGet.

using System;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace EarthRotationAnimation
{
    public partial class MainForm : Form
    {
        private const int FrameWidth = 800;
        private const int FrameHeight = 600;
        private const int RotationInterval = 20;

        private GMapControl mapControl;
        private GMapOverlay markersOverlay;

        public MainForm()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            mapControl = new GMapControl();
            markersOverlay = new GMapOverlay("markersOverlay");

            SuspendLayout();
            mapControl.Dock = DockStyle.Fill;
            mapControl.MapProvider = GMapProviders.OpenStreetMap;
            mapControl.Position = new PointLatLng(0, 0);
            mapControl.Zoom = 1;
            mapControl.MouseWheelZoomEnabled = false;
            mapControl.Overlays.Add(markersOverlay);
            Controls.Add(mapControl);
            ResumeLayout();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Add coastlines as map markers
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(0, 0), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker);
            // Add other coastlines as map markers as needed

            // Start the animation
            int frameCount = 180;
            int frameDelay = RotationInterval;
            for (int frame = 0; frame < frameCount; frame++)
            {
                mapControl.Position = new PointLatLng(0, frame);
                System.Threading.Thread.Sleep(frameDelay);
                Application.DoEvents();
            }
        }
    }
}
