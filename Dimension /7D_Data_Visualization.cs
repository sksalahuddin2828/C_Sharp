using System;
using Plotly.NET;
using Plotly.NET.Layouts;
using Plotly.NET.Traces;

class Program
{
    static void Main()
    {
        // Generate random 7D data with specific patterns
        Random rand = new Random(42);
        int numSamples = 100;
        double[,] data7D = new double[numSamples, 7];
        for (int i = 0; i < numSamples; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                data7D[i, j] = rand.NextDouble();
            }
        }

        // Map the 7D data patterns to colors
        double[,] dataColors = new double[numSamples, 3];
        for (int i = 0; i < numSamples; i++)
        {
            for (int j = 3; j < 6; j++)
            {
                dataColors[i, j - 3] = data7D[i, j];
            }
        }

        // Map the 7D data patterns to sizes
        double[] dataSizes = new double[numSamples];
        for (int i = 0; i < numSamples; i++)
        {
            dataSizes[i] = data7D[i, 0] + data7D[i, 1] + data7D[i, 2];
        }

        // Create the interactive 3D plot with larger figure size
        var fig = new PlotlyChart();
        fig.Layout.Width = 800;
        fig.Layout.Height = 800;

        // Add data points to the 3D plot
        var scatter3D = new Scatter3d()
        {
            x = data7D.GetColumn(0),
            y = data7D.GetColumn(1),
            z = data7D.GetColumn(2),
            mode = "markers",
            marker = new Marker()
            {
                size = dataSizes.Multiply(10),
                color = dataColors,
                colorscale = "Viridis",
                opacity = 0.8
            }
        };
        fig.AddTrace(scatter3D);

        // Set plot layout and axis labels
        fig.Layout.Scene.XAxis.Title = "Dimension 1";
        fig.Layout.Scene.YAxis.Title = "Dimension 2";
        fig.Layout.Scene.ZAxis.Title = "Dimension 3";
        fig.Layout.Scene.XAxis.Range = new[] { 0, 1 };
        fig.Layout.Scene.YAxis.Range = new[] { 0, 1 };
        fig.Layout.Scene.ZAxis.Range = new[] { 0, 1 };

        // Show the interactive plot
        fig.Show();
    }
}
