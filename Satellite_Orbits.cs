using System;
using System.Drawing;
using System.Windows.Forms;

namespace SatelliteOrbits
{
    public partial class Form1 : Form
    {
        private const double EarthRadius = 6371; // Earth radius in kilometers
        private const int NumSatellites = 10;
        private const double SatelliteRadius = 100; // Satellite radius in kilometers
        private const string SatelliteColor = "red";

        private readonly Random random = new Random();

        private readonly double[] semiMajorAxes = new double[NumSatellites];
        private readonly double[] eccentricities = new double[NumSatellites];

        private readonly int numFrames = 100;
        private readonly double[] time;

        public Form1()
        {
            InitializeComponent();

            // Generate random semi-major axes and eccentricities for satellite orbits
            for (int i = 0; i < NumSatellites; i++)
            {
                semiMajorAxes[i] = 800 + random.NextDouble() * (1500 - 800);
                eccentricities[i] = 0.1 + random.NextDouble() * (0.4 - 0.1);
            }

            // Time array
            time = new double[numFrames];
            double step = 2 * Math.PI / numFrames;
            for (int i = 0; i < numFrames; i++)
            {
                time[i] = i * step;
            }

            // Set up the form
            Size = new Size(800, 600);
            Text = "Satellite Orbits";
            Paint += Form1_Paint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            // Plotting the Earth
            graphics.Clear(Color.White);
            double xScale = Width / 2.0 / EarthRadius;
            double yScale = Height / 2.0 / EarthRadius;
            double zScale = Math.Min(xScale, yScale);

            double xCenter = Width / 2.0;
            double yCenter = Height / 2.0;

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    double u = i * 2 * Math.PI / 100;
                    double v = j * Math.PI / 50;
                    double x = xCenter + EarthRadius * Math.Cos(u) * Math.Sin(v) * xScale;
                    double y = yCenter + EarthRadius * Math.Sin(u) * Math.Sin(v) * yScale;
                    double z = EarthRadius * Math.Cos(v) * zScale;
                    graphics.FillRectangle(Brushes.LightBlue, (float)x, (float)y, 1, 1);
                }
            }

            // Plotting the satellite orbits and markers
            for (int i = 0; i < NumSatellites; i++)
            {
                double semiMajorAxis = semiMajorAxes[i];
                double eccentricity = eccentricities[i];

                // Parametric equations for satellite orbit
                double[] r = new double[numFrames];
                double[] xSatellite = new double[numFrames];
                double[] ySatellite = new double[numFrames];
                double[] zSatellite = new double[numFrames];

                for (int j = 0; j < numFrames; j++)
                {
                    r[j] = semiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Math.Cos(time[j]));
                    xSatellite[j] = xCenter + r[j] * Math.Cos(time[j]) * xScale;
                    ySatellite[j] = yCenter + r[j] * Math.Sin(time[j]) * yScale;
                    zSatellite[j] = 0;
                }

                for (int j = 1; j < numFrames; j++)
                {
                    graphics.DrawLine(Pens.Gray, (float)xSatellite[j - 1], (float)ySatellite[j - 1], (float)xSatellite[j], (float)ySatellite[j]);
                }

                // Plotting the satellite markers
                float markerX = (float)xSatellite[numFrames - 1];
                float markerY = (float)ySatellite[numFrames - 1];
                float markerZ = (float)zSatellite[numFrames - 1];
                graphics.FillEllipse(new SolidBrush(Color.Red), markerX - (float)SatelliteRadius, markerY - (float)SatelliteRadius, (float)SatelliteRadius * 2, (float)SatelliteRadius * 2);
            }
        }

        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }
    }
}
