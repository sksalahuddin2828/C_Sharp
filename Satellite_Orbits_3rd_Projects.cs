using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SatelliteOrbits
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Earth parameters
            double earthRadius = 6371;  // Earth radius in kilometers
            double rotationPeriod = 24;  // Earth rotation period in hours

            // Satellite orbit data
            double[][] satellites = {
                new double[] { 800, 0.1, 45, 0, 0 },
                new double[] { 1000, 0.2, 60, 0, 120 },
                new double[] { 1200, 0.3, 75, 0, 240 },
                new double[] { 1400, 0.15, 30, 0, 60 },
                new double[] { 1600, 0.25, 50, 0, 180 }
            };

            // Time array
            int numFrames = 100;
            double[] time = new double[numFrames];
            for (int i = 0; i < numFrames; i++)
            {
                time[i] = 2 * Math.PI * i / (numFrames - 1);
            }

            // Initialize the chart
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());
            chart.Dock = DockStyle.Fill;
            Controls.Add(chart);

            // Earth surface coordinates
            double[] u = new double[100];
            double[] v = new double[50];
            for (int i = 0; i < u.Length; i++)
            {
                u[i] = 2 * Math.PI * i / (u.Length - 1);
            }
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = Math.PI * i / (v.Length - 1);
            }

            double[][] xEarth = new double[u.Length][];
            double[][] yEarth = new double[u.Length][];
            double[][] zEarth = new double[u.Length][];
            for (int i = 0; i < u.Length; i++)
            {
                xEarth[i] = new double[v.Length];
                yEarth[i] = new double[v.Length];
                zEarth[i] = new double[v.Length];
                for (int j = 0; j < v.Length; j++)
                {
                    xEarth[i][j] = earthRadius * Math.Cos(u[i]) * Math.Sin(v[j]);
                    yEarth[i][j] = earthRadius * Math.Sin(u[i]) * Math.Sin(v[j]);
                    zEarth[i][j] = earthRadius * Math.Cos(v[j]);
                }
            }

            // Plot Earth surface
            Series earthSeries = new Series("Earth");
            for (int i = 0; i < u.Length; i++)
            {
                for (int j = 0; j < v.Length; j++)
                {
                    earthSeries.Points.Add(new DataPoint(xEarth[i][j], yEarth[i][j]));
                }
            }
            chart.Series.Add(earthSeries);

            // Initialize satellite lines
            Series[] satelliteSeries = new Series[satellites.Length];
            for (int i = 0; i < satellites.Length; i++)
            {
                satelliteSeries[i] = new Series("Satellite " + (i + 1));
                chart.Series.Add(satelliteSeries[i]);
            }

            // Plotting the satellite orbits
            for (int i = 0; i < satellites.Length; i++)
            {
                double semiMajorAxis = satellites[i][0];
                double eccentricity = satellites[i][1];
                double inclination = satellites[i][2];
                double argumentOfPeriapsis = satellites[i][3];
                double ascendingNode = satellites[i][4];

                // Parametric equations for satellite orbit
                double[] r = new double[numFrames];
                double[] xSatellite = new double[numFrames];
                double[] ySatellite = new double[numFrames];
                double[] zSatellite = new double[numFrames];
                for (int j = 0; j < numFrames; j++)
                {
                    r[j] = semiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Math.Cos(time[j]));
                    xSatellite[j] = r[j] * (Math.Cos(ascendingNode) * Math.Cos(argumentOfPeriapsis + time[j]) - Math.Sin(ascendingNode) * Math.Sin(argumentOfPeriapsis + time[j]) * Math.Cos(inclination));
                    ySatellite[j] = r[j] * (Math.Sin(ascendingNode) * Math.Cos(argumentOfPeriapsis + time[j]) + Math.Cos(ascendingNode) * Math.Sin(argumentOfPeriapsis + time[j]) * Math.Cos(inclination));
                    zSatellite[j] = r[j] * Math.Sin(argumentOfPeriapsis + time[j]) * Math.Sin(inclination);
                }

                // Plot the satellite orbit
                for (int j = 0; j < numFrames; j++)
                {
                    satelliteSeries[i].Points.Add(new DataPoint(xSatellite[j], ySatellite[j]));
                }
            }
        }
    }
}
