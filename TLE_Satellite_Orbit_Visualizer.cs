using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace SatelliteOrbits
{
    public partial class MainWindow : Window
    {
        private List<ModelVisual3D> satellites = new List<ModelVisual3D>();

        private static readonly string[] satelliteNames =
        {
            "ISS (ZARYA)", "Hubble Space Telescope", "GPS IIR-10 (M)", "Terra", "Aqua"
        };

        private static readonly string[][] tleData =
        {
            new []
            {
                "ISS (ZARYA)",
                "1 25544U 98067A   21186.47390511  .00000500  00000-0  13696-4 0  9994",
                "2 25544  51.6431 283.7920 0009298 204.6780 155.3723 15.49061430341806"
            },
            new []
            {
                "Hubble Space Telescope",
                "1 20580U 90037B   21187.51485065  .00000700  00000-0  26089-4 0  9992",
                "2 20580  28.4693 132.2321 0001726 352.6060   7.4753 15.09123027248244"
            },
            new []
            {
                "GPS IIR-10 (M)",
                "1 30793U 07015M   21186.29253259 -.00000037  00000-0  00000-0 0  9999",
                "2 30793  56.1985  60.4463 0064704 249.4686 109.0947  2.00567978134673"
            },
            new []
            {
                "Terra",
                "1 25994U 99068A   21187.36675641  .00000052  00000-0  12124-4 0  9992",
                "2 25994  98.2025 202.0731 0001307  86.8101 273.3496 14.57198865489823"
            },
            new []
            {
                "Aqua",
                "1 27424U 02022A   21187.42292448  .00000100  00000-0  98692-4 0  9999",
                "2 27424  98.2027 189.4386 0001339  88.5354 271.6320 14.57198322495156"
            }
        };

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the Earth
            Model3DGroup earthModelGroup = new Model3DGroup();
            earthModelGroup.Children.Add(new GeometryModel3D(new CylinderGeometry3D(new Point3D(0, 0, 0), 1, 0.02, 100), new DiffuseMaterial(Brushes.Blue)));
            ModelVisual3D earthVisual = new ModelVisual3D();
            earthVisual.Content = earthModelGroup;
            viewport.Children.Add(earthVisual);

            // Add satellites and their orbits to the viewport
            for (int i = 0; i < satelliteNames.Length; i++)
            {
                string name = satelliteNames[i];
                string line1 = tleData[i][1];
                string line2 = tleData[i][2];

                List<Point3D> satPositions = new List<Point3D>();
                for (int j = 0; j < 360; j += 5)
                {
                    DateTime dateTime = new DateTime(2023, 7, 11, j, 0, 0);
                    Point3D position = ComputeSatellitePosition(name, line1, line2, dateTime);
                    satPositions.Add(position);
                }

                // Create satellite orbit path
                Model3DGroup orbitPathModelGroup = new Model3DGroup();
                foreach (var position in satPositions)
                {
                    orbitPathModelGroup.Children.Add(new GeometryModel3D(new SphereGeometry3D(position, 0.02), new DiffuseMaterial(Brushes.Cyan)));
                }
                ModelVisual3D orbitPathVisual = new ModelVisual3D();
                orbitPathVisual.Content = orbitPathModelGroup;
                viewport.Children.Add(orbitPathVisual);

                // Create satellite label
                TextBlock label = CreateSatelliteLabel(name, satPositions[satPositions.Count - 1]);
                viewport.Children.Add(label);

                satellites.Add(orbitPathVisual);
            }

            // Start the animation
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private Point3D ComputeSatellitePosition(string name, string line1, string line2, DateTime dateTime)
        {
            // TODO: Implement satellite position computation using the TLE data and the provided date and time.
            // You can use a suitable library or algorithm to perform the computations.
            // Return the computed satellite position as a Point3D object.
            return new Point3D();
        }

        private TextBlock CreateSatelliteLabel(string name, Point3D position)
        {
            // Create a TextBlock representing the satellite label.
            TextBlock textBlock = new TextBlock();
            textBlock.Text = name;
            textBlock.Foreground = Brushes.White;
            textBlock.FontSize = 12;
            textBlock.FontWeight = FontWeights.Bold;

            // Set the position of the text block
            Point screenPoint = viewport.Project(position);
            Canvas.SetLeft(textBlock, screenPoint.X);
            Canvas.SetTop(textBlock, screenPoint.Y);

            return textBlock;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var satellite in satellites)
            {
                foreach (var model in ((Model3DGroup)satellite.Content).Children)
                {
                    ((GeometryModel3D)model).Transform = ((Transform3DGroup)((GeometryModel3D)model).Transform).Children.RemoveAt(0);
                }
            }
        }
    }
}
