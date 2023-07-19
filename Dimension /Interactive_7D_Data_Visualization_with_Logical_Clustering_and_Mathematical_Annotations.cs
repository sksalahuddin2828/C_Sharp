using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using Plotly;
using Plotly.GraphObjects;
using Plotly.Subplots;
using Accord.MachineLearning;

class Program
{
    static void Main(string[] args)
    {
        int seed = 42;
        int numSamples = 100;
        var rng = new MersenneTwister(seed);
        
        // Generate random 7D data with specific patterns
        Matrix<double> data7D = Matrix<double>.Build.Random(numSamples, 7, rng);

        // Perform clustering on the data
        int numClusters = 3;
        KMeans kmeans = new KMeans(numClusters, randomSeed: seed);
        int[] dataLabels = kmeans.Clustering(data7D.ToRowArrays());

        // Map the 7D data patterns to colors
        int[] dataColors = dataLabels;

        // Map the 7D data patterns to sizes
        double[] dataSizes = new double[numSamples];
        for (int i = 0; i < numSamples; i++)
        {
            dataSizes[i] = data7D.Row(i).SubVector(0, 3).Sum();
        }

        // Create the interactive 3D plot with larger figure size
        var fig = new Subplot(new Scatter3d());
        fig.Width = 800;
        fig.Height = 800;

        // Add data points to the 3D plot
        for (int i = 0; i < numSamples; i++)
        {
            fig.Traces.Add(new Scatter3d()
            {
                X = new[] { data7D[i, 0] },
                Y = new[] { data7D[i, 1] },
                Z = new[] { data7D[i, 2] },
                Mode = "markers",
                Marker = new Marker()
                {
                    Size = dataSizes[i] * 20,
                    Color = dataColors[i].ToString(),
                    ColorScale = "Viridis",
                    Opacity = 0.8
                },
                Text = $"Cluster: {dataLabels[i]}\nData point: {i}",
                HoverInfo = "text"
            });
        }

        // Set plot layout and axis labels
        fig.Layout.Scene.XAxis.Title = "Dimension 1";
        fig.Layout.Scene.YAxis.Title = "Dimension 2";
        fig.Layout.Scene.ZAxis.Title = "Dimension 3";
        fig.Layout.Scene.XAxis.Range = new[] { 0, 1 };
        fig.Layout.Scene.YAxis.Range = new[] { 0, 1 };
        fig.Layout.Scene.ZAxis.Range = new[] { 0, 1 };

        // Show the interactive plot
        Chart.Show(fig);
    }
}
