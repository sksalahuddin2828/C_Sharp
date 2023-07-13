using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Orekit;
using Orekit.Bodies;
using Orekit.Data;
using Orekit.Frames;
using Orekit.Orbits;
using Orekit.Time;
using Orekit.Utils;

namespace SatelliteOrbitVisualization
{
    public partial class MainWindow : Window
    {
        // Define satellite data class
        private class SatelliteData
        {
            public string Name { get; set; }
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public Color Color { get; set; }
        }

        private List<SatelliteData> satelliteDataList;

        public MainWindow()
        {
            InitializeComponent();

            // Read TLE data from a file
            string tleFile = "tle_data.txt";  // Path to the TLE file
            satelliteDataList = new List<SatelliteData>();
            using (StreamReader reader = new StreamReader(tleFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string name = line.Trim();
                    string line1 = reader.ReadLine().Trim();
                    string line2 = reader.ReadLine().Trim();
                    Color color = Colors.Black;  // Set a default color
                    satelliteDataList.Add(new SatelliteData { Name = name, Line1 = line1, Line2 = line2, Color = color });
                }
            }

            // Set up the Viewport3D
            ModelVisual3D scene = new ModelVisual3D();
            viewport3D.Children.Add(scene);

            // Initialize the Earth
            SphereVisual3D earth = new SphereVisual3D
            {
                Center = new Point3D(0, 0, 0),
                Radius = 100,
                Material = new DiffuseMaterial(Brushes.Blue)
            };
            scene.Children.Add(earth);

            // Initialize the satellite paths
            List<CylinderVisual3D> satellitePaths = new List<CylinderVisual3D>();
            foreach (SatelliteData satelliteData in satelliteDataList)
            {
                CylinderVisual3D satellitePath = new CylinderVisual3D
                {
                    Point1 = new Point3D(0, 0, 0),
                    Point2 = new Point3D(0, 0, 1000),
                    Diameter = 0.2,
                    Material = new DiffuseMaterial(new SolidColorBrush(satelliteData.Color))
                };
                scene.Children.Add(satellitePath);
                satellitePaths.Add(satellitePath);

                // Label each satellite
                TextVisual3D satelliteLabel = new TextVisual3D
                {
                    Position = new Point3D(0, 0, 1000),
                    Text = satelliteData.Name,
                    Foreground = new SolidColorBrush(satelliteData.Color),
                    FontSize = 12
                };
                scene.Children.Add(satelliteLabel);
            }

            // Create an animation timer
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += (sender, e) =>
            {
                double t = DateTime.Now.TimeOfDay.TotalSeconds; // Time in seconds

                // Update satellite paths
                for (int i = 0; i < satelliteDataList.Count; i++)
                {
                    SatelliteData satelliteData = satelliteDataList[i];
                    Orbit orbit = CreateOrbit(satelliteData.Line1, satelliteData.Line2);
                    PVCoordinates pvCoordinates = orbit.GetPVCoordinates(GetDate(t));
                    double x = pvCoordinates.Position.X / 1_000_000; // Scale the coordinates
                    double y = pvCoordinates.Position.Y / 1_000_000;
                    double z = pvCoordinates.Position.Z / 1_000_000;

                    CylinderVisual3D satellitePath = satellitePaths[i];
                    satellitePath.Point1 = new Point3D(0, 0, 0);
                    satellitePath.Point2 = new Point3D(x, y, z);
                }
            };
            timer.Start();
        }

        private AbsoluteDate GetDate(double t)
        {
            return AbsoluteDate.J2000_EPOCH.ShiftedBy(t);
        }

        private Orbit CreateOrbit(string line1, string line2)
        {
            try
            {
                DataProvidersManager manager = DataProvidersManager.Instance;
                manager.AddProvider(new DirectoryCrawler(new FileInfo(".")));

                TLE tle = new TLE(line1, line2);
                Frame earthFrame = FramesFactory.GetICRF();
                Orbit orbit = tle.GetOrbit();

                return orbit.ShiftedBy(GetDate(0)).ChangeFrame(earthFrame);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
