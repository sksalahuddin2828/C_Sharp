using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace TesseractVisualization
{
    public class TesseractVisualizationForm : Form
    {
        private readonly double[][] vertices = new double[][]
        {
            new double[] { -1, -1, -1, -1, -1, 1, -1 },
            new double[] { -1, -1, -1, -1, -1, 1, 1 },
            new double[] { -1, -1, -1, -1, 1, -1, -1 },
            new double[] { -1, -1, -1, -1, 1, -1, 1 },
            new double[] { -1, -1, -1, -1, 1, 1, -1 },
            new double[] { -1, -1, -1, -1, 1, 1, 1 },
            new double[] { -1, -1, -1, 1, -1, -1, -1 },
            new double[] { -1, -1, -1, 1, -1, -1, 1 },
            new double[] { -1, -1, -1, 1, -1, 1, -1 },
            new double[] { -1, -1, -1, 1, -1, 1, 1 },
            new double[] { -1, -1, -1, 1, 1, -1, -1 },
            new double[] { -1, -1, -1, 1, 1, -1, 1 },
            new double[] { -1, -1, -1, 1, 1, 1, -1 },
            new double[] { -1, -1, -1, 1, 1, 1, 1 },
            new double[] { -1, -1, 1, -1, -1, -1, -1 },
            new double[] { -1, -1, 1, -1, -1, -1, 1 },
            new double[] { -1, -1, 1, -1, -1, 1, -1 },
            new double[] { -1, -1, 1, -1, -1, 1, 1 },
            new double[] { -1, -1, 1, -1, 1, -1, -1 },
            new double[] { -1, -1, 1, -1, 1, -1, 1 },
            new double[] { -1, -1, 1, -1, 1, 1, -1 },
            new double[] { -1, -1, 1, -1, 1, 1, 1 },
            new double[] { -1, -1, 1, 1, -1, -1, -1 },
            new double[] { -1, -1, 1, 1, -1, -1, 1 },
            new double[] { -1, -1, 1, 1, -1, 1, -1 },
            new double[] { -1, -1, 1, 1, -1, 1, 1 },
            new double[] { -1, -1, 1, 1, 1, -1, -1 },
            new double[] { -1, -1, 1, 1, 1, -1, 1 },
            new double[] { -1, -1, 1, 1, 1, 1, -1 },
            new double[] { -1, -1, 1, 1, 1, 1, 1 },
            new double[] { -1, 1, -1, -1, -1, -1, -1 },
            new double[] { -1, 1, -1, -1, -1, -1, 1 },
            new double[] { -1, 1, -1, -1, -1, 1, -1 },
            new double[] { -1, 1, -1, -1, -1, 1, 1 },
            new double[] { -1, 1, -1, -1, 1, -1, -1 },
            new double[] { -1, 1, -1, -1, 1, -1, 1 },
            new double[] { -1, 1, -1, -1, 1, 1, -1 },
            new double[] { -1, 1, -1, -1, 1, 1, 1 },
            new double[] { -1, 1, -1, 1, -1, -1, -1 },
            new double[] { -1, 1, -1, 1, -1, -1, 1 },
            new double[] { -1, 1, -1, 1, -1, 1, -1 },
            new double[] { -1, 1, -1, 1, -1, 1, 1 },
            new double[] { -1, 1, -1, 1, 1, -1, -1 },
            new double[] { -1, 1, -1, 1, 1, -1, 1 },
            new double[] { -1, 1, -1, 1, 1, 1, -1 },
            new double[] { -1, 1, -1, 1, 1, 1, 1 },
            new double[] { -1, 1, 1, -1, -1, -1, -1 },
            new double[] { -1, 1, 1, -1, -1, -1, 1 },
            new double[] { -1, 1, 1, -1, -1, 1, -1 },
            new double[] { -1, 1, 1, -1, -1, 1, 1 },
            new double[] { -1, 1, 1, -1, 1, -1, -1 },
            new double[] { -1, 1, 1, -1, 1, -1, 1 },
            new double[] { -1, 1, 1, -1, 1, 1, -1 },
            new double[] { -1, 1, 1, -1, 1, 1, 1 },
            new double[] { -1, 1, 1, 1, -1, -1, -1 },
            new double[] { -1, 1, 1, 1, -1, -1, 1 },
            new double[] { -1, 1, 1, 1, -1, 1, -1 },
            new double[] { -1, 1, 1, 1, -1, 1, 1 },
            new double[] { -1, 1, 1, 1, 1, -1, -1 },
            new double[] { -1, 1, 1, 1, 1, -1, 1 },
            new double[] { -1, 1, 1, 1, 1, 1, -1 },
            new double[] { -1, 1, 1, 1, 1, 1, 1 },
            new double[] { 1, -1, -1, -1, -1, -1, -1 },
            new double[] { 1, -1, -1, -1, -1, -1, 1 },
            new double[] { 1, -1, -1, -1, -1, 1, -1 },
            new double[] { 1, -1, -1, -1, -1, 1, 1 },
            new double[] { 1, -1, -1, -1, 1, -1, -1 },
            new double[] { 1, -1, -1, -1, 1, -1, 1 },
            new double[] { 1, -1, -1, -1, 1, 1, -1 },
            new double[] { 1, -1, -1, -1, 1, 1, 1 },
            new double[] { 1, -1, -1, 1, -1, -1, -1 },
            new double[] { 1, -1, -1, 1, -1, -1, 1 },
            new double[] { 1, -1, -1, 1, -1, 1, -1 },
            new double[] { 1, -1, -1, 1, -1, 1, 1 },
            new double[] { 1, -1, -1, 1, 1, -1, -1 },
            new double[] { 1, -1, -1, 1, 1, -1, 1 },
            new double[] { 1, -1, -1, 1, 1, 1, -1 },
            new double[] { 1, -1, -1, 1, 1, 1, 1 },
            new double[] { 1, -1, 1, -1, -1, -1, -1 },
            new double[] { 1, -1, 1, -1, -1, -1, 1 },
            new double[] { 1, -1, 1, -1, -1, 1, -1 },
            new double[] { 1, -1, 1, -1, -1, 1, 1 },
            new double[] { 1, -1, 1, -1, 1, -1, -1 },
            new double[] { 1, -1, 1, -1, 1, -1, 1 },
            new double[] { 1, -1, 1, -1, 1, 1, -1 },
            new double[] { 1, -1, 1, -1, 1, 1, 1 },
            new double[] { 1, -1, 1, 1, -1, -1, -1 },
            new double[] { 1, -1, 1, 1, -1, -1, 1 },
            new double[] { 1, -1, 1, 1, -1, 1, -1 },
            new double[] { 1, -1, 1, 1, -1, 1, 1 },
            new double[] { 1, -1, 1, 1, 1, -1, -1 },
            new double[] { 1, -1, 1, 1, 1, -1, 1 },
            new double[] { 1, -1, 1, 1, 1, 1, -1 },
            new double[] { 1, -1, 1, 1, 1, 1, 1 },
            new double[] { 1, 1, -1, -1, -1, -1, -1 },
            new double[] { 1, 1, -1, -1, -1, -1, 1 },
            new double[] { 1, 1, -1, -1, -1, 1, -1 },
            new double[] { 1, 1, -1, -1, -1, 1, 1 },
            new double[] { 1, 1, -1, -1, 1, -1, -1 },
            new double[] { 1, 1, -1, -1, 1, -1, 1 },
            new double[] { 1, 1, -1, -1, 1, 1, -1 },
            new double[] { 1, 1, -1, -1, 1, 1, 1 },
            new double[] { 1, 1, -1, 1, -1, -1, -1 },
            new double[] { 1, 1, -1, 1, -1, -1, 1 },
            new double[] { 1, 1, -1, 1, -1, 1, -1 },
            new double[] { 1, 1, -1, 1, -1, 1, 1 },
            new double[] { 1, 1, -1, 1, 1, -1, -1 },
            new double[] { 1, 1, -1, 1, 1, -1, 1 },
            new double[] { 1, 1, -1, 1, 1, 1, -1 },
            new double[] { 1, 1, -1, 1, 1, 1, 1 },
            new double[] { 1, 1, 1, -1, -1, -1, -1 },
            new double[] { 1, 1, 1, -1, -1, -1, 1 },
            new double[] { 1, 1, 1, -1, -1, 1, -1 },
            new double[] { 1, 1, 1, -1, -1, 1, 1 },
            new double[] { 1, 1, 1, -1, 1, -1, -1 },
            new double[] { 1, 1, 1, -1, 1, -1, 1 },
            new double[] { 1, 1, 1, -1, 1, 1, -1 },
            new double[] { 1, 1, 1, -1, 1, 1, 1 },
            new double[] { 1, 1, 1, 1, -1, -1, -1 },
            new double[] { 1, 1, 1, 1, -1, -1, 1 },
            new double[] { 1, 1, 1, 1, -1, 1, -1 },
            new double[] { 1, 1, 1, 1, -1, 1, 1 },
            new double[] { 1, 1, 1, 1, 1, -1, -1 },
            new double[] { 1, 1, 1, 1, 1, -1, 1 },
            new double[] { 1, 1, 1, 1, 1, 1, -1 },
            new double[] { 1, 1, 1, 1, 1, 1, 1 }
        };

        private readonly int[][] edges = new int[][]
        {
            new int[] { 0, 3 },
            new int[] { 0, 4 },
            new int[] { 0, 5 },
            new int[] { 0, 6 },
            new int[] { 0, 7 },
            new int[] { 1, 2 },
            new int[] { 1, 3 },
            new int[] { 1, 4 },
            new int[] { 1, 5 },
            new int[] { 1, 6 },
            new int[] { 1, 7 },
            new int[] { 2, 3 },
            new int[] { 2, 4 },
            new int[] { 2, 5 },
            new int[] { 2, 6 },
            new int[] { 2, 7 },
            new int[] { 3, 4 },
            new int[] { 3, 5 },
            new int[] { 3, 6 },
            new int[] { 3, 7 },
            new int[] { 4, 5 },
            new int[] { 4, 6 },
            new int[] { 4, 7 },
            new int[] { 5, 6 },
            new int[] { 5, 7 },
            new int[] { 6, 7 },
            new int[] { 8, 9 },
            new int[] { 8, 10 },
            new int[] { 8, 11 },
            new int[] { 8, 12 },
            new int[] { 8, 13 },
            new int[] { 8, 14 },
            new int[] { 8, 15 },
            new int[] { 9, 10 },
            new int[] { 9, 11 },
            new int[] { 9, 12 },
            new int[] { 9, 13 },
            new int[] { 9, 14 },
            new int[] { 9, 15 },
            new int[] { 10, 11 },
            new int[] { 10, 12 },
            new int[] { 10, 13 },
            new int[] { 10, 14 },
            new int[] { 10, 15 },
            new int[] { 11, 12 },
            new int[] { 11, 13 },
            new int[] { 11, 14 },
            new int[] { 11, 15 },
            new int[] { 12, 13 },
            new int[] { 12, 14 },
            new int[] { 12, 15 },
            new int[] { 13, 14 },
            new int[] { 13, 15 },
            new int[] { 14, 15 },
            new int[] { 16, 17 },
            new int[] { 16, 18 },
            new int[] { 16, 19 },
            new int[] { 16, 20 },
            new int[] { 16, 21 },
            new int[] { 16, 22 },
            new int[] { 16, 23 },
            new int[] { 17, 18 },
            new int[] { 17, 19 },
            new int[] { 17, 20 },
            new int[] { 17, 21 },
            new int[] { 17, 22 },
            new int[] { 17, 23 },
            new int[] { 18, 19 },
            new int[] { 18, 20 },
            new int[] { 18, 21 },
            new int[] { 18, 22 },
            new int[] { 18, 23 },
            new int[] { 19, 20 },
            new int[] { 19, 21 },
            new int[] { 19, 22 },
            new int[] { 19, 23 },
            new int[] { 20, 21 },
            new int[] { 20, 22 },
            new int[] { 20, 23 },
            new int[] { 21, 22 },
            new int[] { 21, 23 },
            new int[] { 22, 23 },
            new int[] { 24, 25 },
            new int[] { 24, 26 },
            new int[] { 24, 27 },
            new int[] { 24, 28 },
            new int[] { 24, 29 },
            new int[] { 24, 30 },
            new int[] { 24, 31 },
            new int[] { 25, 26 },
            new int[] { 25, 27 },
            new int[] { 25, 28 },
            new int[] { 25, 29 },
            new int[] { 25, 30 },
            new int[] { 25, 31 },
            new int[] { 26, 27 },
            new int[] { 26, 28 },
            new int[] { 26, 29 },
            new int[] { 26, 30 },
            new int[] { 26, 31 },
            new int[] { 27, 28 },
            new int[] { 27, 29 },
            new int[] { 27, 30 },
            new int[] { 27, 31 },
            new int[] { 28, 29 },
            new int[] { 28, 30 },
            new int[] { 28, 31 },
            new int[] { 29, 30 },
            new int[] { 29, 31 },
            new int[] { 30, 31 }
        };

        public TesseractVisualizationForm()
        {
            Size = new Size(800, 800);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Define rotation angle
            double angle = Math.PI / 4;

            // Define rotation matrix for the first three dimensions
            double[][] rotationMatrix3D = new double[][]
            {
                new double[] { Math.Cos(angle), 0, -Math.Sin(angle) },
                new double[] { 0, Math.Cos(angle), 0 },
                new double[] { Math.Sin(angle), 0, Math.Cos(angle) }
            };

            // Project vertices onto 3D space
            double[][] projectedVertices3D = vertices.Select(v => MultiplyMatrix(rotationMatrix3D, v.Take(3).ToArray())).ToArray();

            // Define rotation matrix for the fourth, fifth, sixth, and seventh dimensions
            double[][] rotationMatrix4567 = new double[][]
            {
                new double[] { 1, 0, 0 },
                new double[] { 0, Math.Cos(angle), -Math.Sin(angle) },
                new double[] { 0, Math.Sin(angle), Math.Cos(angle) }
            };

            // Project vertices from 3D space to the fourth, fifth, sixth, and seventh dimensions
            double[][] projectedVertices4567 = projectedVertices3D.Select(v => MultiplyMatrix(rotationMatrix4567, v)).ToArray();

            // Plot projected vertices with labels
            string[] labels = vertices.Select(v => string.Join("", v)).ToArray();
            for (int i = 0; i < projectedVertices3D.Length; i++)
            {
                float x = (float)projectedVertices3D[i][0] * 200 + Width / 2;
                float y = (float)projectedVertices3D[i][1] * 200 + Height / 2;
                g.FillEllipse(Brushes.Red, x - 5, y - 5, 10, 10);
                g.DrawString(labels[i], Font, Brushes.Black, x, y);
            }

            // Create illusion lines connecting projected vertices in 3D space
            for (int i = 0; i < projectedVertices3D.Length; i++)
            {
                for (int j = i + 1; j < projectedVertices3D.Length; j++)
                {
                    float x1 = (float)projectedVertices3D[i][0] * 200 + Width / 2;
                    float y1 = (float)projectedVertices3D[i][1] * 200 + Height / 2;
                    float x2 = (float)projectedVertices3D[j][0] * 200 + Width / 2;
                    float y2 = (float)projectedVertices3D[j][1] * 200 + Height / 2;
                    g.DrawLine(Pens.Black, x1, y1, x2, y2);
                }
            }
        }

        private double[] MultiplyMatrix(double[][] matrix, double[] vector)
        {
            double[] result = new double[matrix.Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                result[i] = matrix[i].Zip(vector, (a, b) => a * b).Sum();
            }
            return result;
        }

        [STAThread]
        static void Main()
        {
            Application.Run(new TesseractVisualizationForm());
        }
    }
}
