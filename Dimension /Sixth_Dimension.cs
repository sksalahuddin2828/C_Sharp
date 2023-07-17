using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using ScottPlot;

class TesseractPlot
{
    static void Main(string[] args)
    {
        // Define tesseract vertices with the sixth dimension
        Matrix<double> vertices = DenseMatrix.OfArray(new double[,]
        {
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, 1 },
            { -1, -1, -1, -1, 1, -1 },
            { -1, -1, -1, -1, 1, 1 },
            { -1, -1, -1, 1, -1, -1 },
            { -1, -1, -1, 1, -1, 1 },
            { -1, -1, -1, 1, 1, -1 },
            { -1, -1, -1, 1, 1, 1 },
            { -1, 1, -1, -1, -1, -1 },
            { -1, 1, -1, -1, -1, 1 },
            { -1, 1, -1, -1, 1, -1 },
            { -1, 1, -1, -1, 1, 1 },
            { -1, 1, 1, -1, -1, -1 },
            { -1, 1, 1, -1, -1, 1 },
            { -1, 1, 1, -1, 1, -1 },
            { -1, 1, 1, -1, 1, 1 },
            { 1, -1, -1, -1, -1, -1 },
            { 1, -1, -1, -1, -1, 1 },
            { 1, -1, -1, -1, 1, -1 },
            { 1, -1, -1, -1, 1, 1 },
            { 1, -1, 1, -1, -1, -1 },
            { 1, -1, 1, -1, -1, 1 },
            { 1, -1, 1, -1, 1, -1 },
            { 1, -1, 1, -1, 1, 1 },
            { 1, 1, -1, -1, -1, -1 },
            { 1, 1, -1, -1, -1, 1 },
            { 1, 1, -1, -1, 1, -1 },
            { 1, 1, -1, -1, 1, 1 },
            { 1, 1, 1, -1, -1, -1 },
            { 1, 1, 1, -1, -1, 1 },
            { 1, 1, 1, -1, 1, -1 },
            { 1, 1, 1, -1, 1, 1 }
        });

        // Define edges of the tesseract
        Tuple<int, int>[] edges = {
            Tuple.Create(0, 1), Tuple.Create(0, 2), Tuple.Create(0, 4), Tuple.Create(1, 3),
            Tuple.Create(1, 5), Tuple.Create(2, 3), Tuple.Create(2, 6), Tuple.Create(3, 7),
            Tuple.Create(4, 5), Tuple.Create(4, 6), Tuple.Create(5, 7), Tuple.Create(6, 7),
            Tuple.Create(8, 9), Tuple.Create(8, 10), Tuple.Create(8, 12), Tuple.Create(9, 11),
            Tuple.Create(9, 13), Tuple.Create(10, 11), Tuple.Create(10, 14), Tuple.Create(11, 15),
            Tuple.Create(12, 13), Tuple.Create(12, 14), Tuple.Create(13, 15), Tuple.Create(14, 15),
            Tuple.Create(0, 8), Tuple.Create(1, 9), Tuple.Create(2, 10), Tuple.Create(3, 11),
            Tuple.Create(4, 12), Tuple.Create(5, 13), Tuple.Create(6, 14), Tuple.Create(7, 15)
        };

        // Create a plot
        var plt = new Plot3D();
        plt.PlotSize(800, 800);
        plt.Ticks(displayTicks: false);

        // Plot the tesseract edges
        foreach (var edge in edges)
        {
            var x = new double[] { vertices[edge.Item1, 0], vertices[edge.Item2, 0] };
            var y = new double[] { vertices[edge.Item1, 1], vertices[edge.Item2, 1] };
            var z = new double[] { vertices[edge.Item1, 2], vertices[edge.Item2, 2] };
            plt.AddScatter(x, y, z, color: Color.Black);
        }

        // Define rotation matrix for the first three dimensions
        double angle = Math.PI / 4;
        Matrix<double> rotationMatrix3D = DenseMatrix.OfArray(new double[,]
        {
            { Math.Cos(angle), 0, -Math.Sin(angle) },
            { 0, Math.Cos(angle), 0 },
            { Math.Sin(angle), 0, Math.Cos(angle) }
        });

        // Project vertices onto 3D space
        var projectedVertices3D = vertices.SubMatrix(0, vertices.RowCount, 0, 3).Multiply(rotationMatrix3D);

        // Define rotation matrix for the fourth, fifth, and sixth dimensions
        Matrix<double> rotationMatrix456 = DenseMatrix.OfArray(new double[,]
        {
            { 1, 0, 0 },
            { 0, Math.Cos(angle), -Math.Sin(angle) },
            { 0, Math.Sin(angle), Math.Cos(angle) }
        });

        // Project vertices from 3D space to the fourth, fifth, and sixth dimensions
        var projectedVertices456 = projectedVertices3D.Multiply(rotationMatrix456);

        // Plot projected vertices with labels
        var labels = new string[vertices.RowCount];
        for (int i = 0; i < vertices.RowCount; i++)
        {
            labels[i] = string.Join("", vertices.Row(i).Select(v => v.ToString()));
        }
        plt.AddScatter(projectedVertices3D.Column(0).ToArray(), projectedVertices3D.Column(1).ToArray(),
            projectedVertices3D.Column(2).ToArray(), colorByValue: projectedVertices456.Column(2).ToArray(),
            colorMap: Colormap.Viridis, markerSize: 5, label: labels);

        // Create illusion lines connecting projected vertices in 3D space
        for (int i = 0; i < projectedVertices3D.RowCount; i++)
        {
            for (int j = i + 1; j < projectedVertices3D.RowCount; j++)
            {
                double[] x = { projectedVertices3D[i, 0], projectedVertices3D[j, 0] };
                double[] y = { projectedVertices3D[i, 1], projectedVertices3D[j, 1] };
                double[] z = { projectedVertices3D[i, 2], projectedVertices3D[j, 2] };
                plt.AddLine(x, y, z, color: Color.Black, style: LineStyle.Dot, lineWidth: 1, alpha: 0.3);
            }
        }

        // Add a color bar for the sixth dimension
        plt.AddColorbar(label: "Sixth Dimension");

        // Save or show the plot
        plt.SaveFig("tesseract_plot.png");
        plt.Show();
    }
}
