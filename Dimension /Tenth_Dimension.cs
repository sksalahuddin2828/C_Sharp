using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;
using OxyPlot.Series;

class HypercubePlot
{
    static Matrix<double> GenerateHypercubeVertices(int dimensions)
    {
        var range = Vector<double>.Build.DenseOfArray(Enumerable.Repeat(-1.0, dimensions).ToArray())
            .ToColumnMatrix();
        var ones = Vector<double>.Build.DenseOfArray(Enumerable.Repeat(1.0, dimensions).ToArray())
            .ToColumnMatrix();
        var vertices = Matrix<double>.Build.Dense(dimensions, (int)Math.Pow(2, dimensions));

        for (int i = 0; i < dimensions; i++)
        {
            var stride = (int)Math.Pow(2, i);
            for (int j = 0; j < (int)Math.Pow(2, dimensions - i - 1); j++)
            {
                var segment = Vector<double>.Build.DenseOfArray(
                    Enumerable.Repeat(stride, stride).ToArray());
                vertices.SetSubMatrix(i, stride * j, 1, stride, segment + j * stride * ones);
            }
        }

        return vertices.Transpose();
    }

    static Matrix<double> GenerateHypercubeEdges(int dimensions)
    {
        var vertices = GenerateHypercubeVertices(dimensions);
        var edges = Matrix<double>.Build.Dense(0, 2);
        for (int i = 0; i < vertices.RowCount; i++)
        {
            for (int j = i + 1; j < vertices.RowCount; j++)
            {
                if (vertices.Row(i).Zip(vertices.Row(j), (a, b) => Math.Abs(a - b)).Sum() == 2)
                {
                    edges = edges.Stack(DenseMatrix.OfRowVectors(vertices.Row(i), vertices.Row(j)));
                }
            }
        }
        return edges;
    }

    static void Main(string[] args)
    {
        // Define the number of dimensions for the hypercube
        int numDimensions = 10;

        // Generate hypercube vertices and edges
        var vertices = GenerateHypercubeVertices(numDimensions);
        var edges = GenerateHypercubeEdges(numDimensions);

        // Create a plot model
        var plotModel = new PlotModel { Title = "10D Hypercube Projection" };

        // Plot the 10D hypercube edges
        for (int i = 0; i < edges.RowCount; i++)
        {
            var edgeX = new LineSeries
            {
                Title = "Edge " + i,
                StrokeThickness = 1,
                Color = OxyColors.Black
            };

            edgeX.Points.Add(new DataPoint(vertices[edges[i, 0], 0], vertices[edges[i, 0], 1]));
            edgeX.Points.Add(new DataPoint(vertices[edges[i, 1], 0], vertices[edges[i, 1], 1]));
            plotModel.Series.Add(edgeX);

            var edgeY = new LineSeries
            {
                StrokeThickness = 1,
                Color = OxyColors.Black
            };

            edgeY.Points.Add(new DataPoint(vertices[edges[i, 0], 2], vertices[edges[i, 0], 3]));
            edgeY.Points.Add(new DataPoint(vertices[edges[i, 1], 2], vertices[edges[i, 1], 3]));
            plotModel.Series.Add(edgeY);

            var edgeZ = new LineSeries
            {
                StrokeThickness = 1,
                Color = OxyColors.Black
            };

            edgeZ.Points.Add(new DataPoint(vertices[edges[i, 0], 4], vertices[edges[i, 0], 5]));
            edgeZ.Points.Add(new DataPoint(vertices[edges[i, 1], 4], vertices[edges[i, 1], 5]));
            plotModel.Series.Add(edgeZ);

            var edgeW = new LineSeries
            {
                StrokeThickness = 1,
                Color = OxyColors.Black
            };

            edgeW.Points.Add(new DataPoint(vertices[edges[i, 0], 6], vertices[edges[i, 0], 7]));
            edgeW.Points.Add(new DataPoint(vertices[edges[i, 1], 6], vertices[edges[i, 1], 7]));
            plotModel.Series.Add(edgeW);

            var edgeV = new LineSeries
            {
                StrokeThickness = 1,
                Color = OxyColors.Black
            };

            edgeV.Points.Add(new DataPoint(vertices[edges[i, 0], 8], vertices[edges[i, 0], 9]));
            edgeV.Points.Add(new DataPoint(vertices[edges[i, 1], 8], vertices[edges[i, 1], 9]));
            plotModel.Series.Add(edgeV);
        }

        // Create the plot view and display it
        var plotView = new OxyPlot.WindowsForms.PlotView { Model = plotModel };
        plotView.Dock = System.Windows.Forms.DockStyle.Fill;

        // Add the plot view to a Windows Forms or WPF container to display it
        // For Windows Forms, use: form.Controls.Add(plotView);
        // For WPF, use: someGrid.Children.Add(plotView);

        // Example of Windows Forms usage:
        var form = new System.Windows.Forms.Form { Width = 800, Height = 600 };
        form.Controls.Add(plotView);
        form.ShowDialog();
    }
}




\\-----------------------------------------------------------------------------------------------------------------------
\\------------------------------------------------Windows Forms Usage:---------------------------------------------------
\\-----------------------------------------------------------------------------------------------------------------------

    

using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;
using OxyPlot.Series;

class HypercubePlot
{
    // ... (The rest of the code remains unchanged)

    static void Main(string[] args)
    {
        // ... (The rest of the code remains unchanged)

        // Add the plot view to a Windows Forms container to display it
        var form = new System.Windows.Forms.Form { Width = 800, Height = 600 };
        form.Controls.Add(plotView); // Add the OxyPlot view to the form
        form.ShowDialog();
    }
}



\\-----------------------------------------------------------------------------------------------------------------------
\\-----------------------------------------------------WPF Usage:--------------------------------------------------------
\\-----------------------------------------------------------------------------------------------------------------------

    

using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;
using OxyPlot.Series;
using System.Windows;

class HypercubePlot
{
    // ... (The rest of the code remains unchanged)

    static void Main(string[] args)
    {
        // ... (The rest of the code remains unchanged)

        // Add the plot view to a WPF container to display it
        var window = new Window { Width = 800, Height = 600 };
        var grid = new System.Windows.Controls.Grid();
        window.Content = grid;

        // Add the OxyPlot view to the grid
        grid.Children.Add(plotView);

        // Show the WPF window
        window.ShowDialog();
    }
}
    
