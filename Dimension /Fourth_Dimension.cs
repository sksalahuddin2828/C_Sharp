using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace TesseractPlot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define tesseract vertices
            double[,] vertices = new double[,]
            {
                {-1, -1, -1, -1},
                {-1, -1, -1,  1},
                {-1, -1,  1, -1},
                {-1, -1,  1,  1},
                {-1,  1, -1, -1},
                {-1,  1, -1,  1},
                {-1,  1,  1, -1},
                {-1,  1,  1,  1},
                { 1, -1, -1, -1},
                { 1, -1, -1,  1},
                { 1, -1,  1, -1},
                { 1, -1,  1,  1},
                { 1,  1, -1, -1},
                { 1,  1, -1,  1},
                { 1,  1,  1, -1},
                { 1,  1,  1,  1}
            };

            // Define edges of the tesseract
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>()
            {
                Tuple.Create(0, 1), Tuple.Create(0, 2), Tuple.Create(0, 4), Tuple.Create(1, 3),
                Tuple.Create(1, 5), Tuple.Create(2, 3), Tuple.Create(2, 6), Tuple.Create(3, 7),
                Tuple.Create(4, 5), Tuple.Create(4, 6), Tuple.Create(5, 7), Tuple.Create(6, 7),
                Tuple.Create(8, 9), Tuple.Create(8, 10), Tuple.Create(8, 12), Tuple.Create(9, 11),
                Tuple.Create(9, 13), Tuple.Create(10, 11), Tuple.Create(10, 14), Tuple.Create(11, 15),
                Tuple.Create(12, 13), Tuple.Create(12, 14), Tuple.Create(13, 15), Tuple.Create(14, 15),
                Tuple.Create(0, 8), Tuple.Create(1, 9), Tuple.Create(2, 10), Tuple.Create(3, 11),
                Tuple.Create(4, 12), Tuple.Create(5, 13), Tuple.Create(6, 14), Tuple.Create(7, 15)
            };

            // Create a chart control
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());
            chart.Size = new System.Drawing.Size(800, 800);

            // Plot the tesseract edges
            foreach (var edge in edges)
            {
                double[] xValues = new double[] { vertices[edge.Item1, 0], vertices[edge.Item2, 0] };
                double[] yValues = new double[] { vertices[edge.Item1, 1], vertices[edge.Item2, 1] };
                double[] zValues = new double[] { vertices[edge.Item1, 2], vertices[edge.Item2, 2] };

                chart.Series.Add(new Series
                {
                    ChartType = SeriesChartType.Line,
                    Points.DataBindXYZ(xValues, yValues, zValues, null),
                    Color = System.Drawing.Color.Black
                });
            }

            // Define rotation matrix
            double angle = Math.PI / 4;
            double[,] rotationMatrix = new double[,]
            {
                { Math.Cos(angle), 0, -Math.Sin(angle), 0 },
                { 0, Math.Cos(angle), 0, -Math.Sin(angle) },
                { Math.Sin(angle), 0, Math.Cos(angle), 0 },
                { 0, Math.Sin(angle), 0, Math.Cos(angle) }
            };

            // Project vertices onto 3D space
            double[,] projectedVertices = MatrixMultiply(vertices, rotationMatrix);

            // Plot projected vertices with labels
            string[] labels = new string[vertices.GetLength(0)];
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                labels[i] = string.Join("", Enumerable.Range(0, vertices.GetLength(1))
                    .Select(j => vertices[i, j].ToString())
                    .ToArray());

                chart.Series.Add(new Series
                {
                    ChartType = SeriesChartType.Point,
                    Points = { new DataPoint(projectedVertices[i, 0], projectedVertices[i, 1], projectedVertices[i, 2]) }
                });

                TextAnnotation annotation = new TextAnnotation();
                annotation.Text = labels[i];
                annotation.AnchorDataPoint = chart.Series.Last().Points[0];
                annotation.Font = new System.Drawing.Font("Arial", 8);
                annotation.ForeColor = System.Drawing.Color.Black;
                annotation.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
                chart.Annotations.Add(annotation);
            }

            // Create illusion lines connecting projected vertices
            for (int i = 0; i < projectedVertices.GetLength(0); i++)
            {
                for (int j = i + 1; j < projectedVertices.GetLength(0); j++)
                {
                    chart.Series.Add(new Series
                    {
                        ChartType = SeriesChartType.Line,
                        Points =
                        {
                            new DataPoint(projectedVertices[i, 0], projectedVertices[i, 1], projectedVertices[i, 2]),
                            new DataPoint(projectedVertices[j, 0], projectedVertices[j, 1], projectedVertices[j, 2])
                        },
                        Color = System.Drawing.Color.Black,
                        BorderDashStyle = ChartDashStyle.Dash,
                        BorderWidth = 1,
                        BorderColor = System.Drawing.Color.Gray
                    });
                }
            }

            // Add a color bar
            chart.Legends.Add(new Legend());
            chart.Legends[0].Docking = Docking.Right;
            chart.Series.Add(new Series
            {
                ChartType = SeriesChartType.Point,
                MarkerSize = 10,
                Points = { new DataPoint(0, 0) },
                ColorMap = new ColorMap(new[] { 0, 1 }, new[] { System.Drawing.Color.Violet, System.Drawing.Color.Yellow })
            });

            // Display the chart
            Form form = new Form();
            form.Controls.Add(chart);
            Application.Run(form);
        }

        static double[,] MatrixMultiply(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int cols2 = matrix2.GetLength(1);
            double[,] result = new double[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < cols1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }
    }
}
