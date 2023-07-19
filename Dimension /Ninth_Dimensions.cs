using System;
using System.Drawing;
using System.Windows.Forms;

namespace HypercubePlot
{
    public class HypercubePlotForm : Form
    {
        private int numDimensions = 9;
        private int[] vertexValues = { -1, 1 };
        private double angle = Math.PI / 4;

        private int[][] vertices;
        private int[][] edges;

        private int[][] GenerateHypercubeVertices(int dimensions)
        {
            int numVertices = (int)Math.Pow(2, dimensions);
            int numPerSide = numVertices / 2;

            int[][] result = new int[numVertices][];
            for (int i = 0; i < numVertices; i++)
            {
                result[i] = new int[dimensions];
                for (int j = 0; j < dimensions; j++)
                {
                    int value = vertexValues[(i / numPerSide) % 2];
                    result[i][j] = value;
                    numPerSide /= 2;
                }
            }
            return result;
        }

        private int[][] GenerateHypercubeEdges(int dimensions)
        {
            int numVertices = (int)Math.Pow(2, dimensions);
            int numEdges = dimensions * numVertices / 2;

            int[][] result = new int[numEdges][];
            int edgeIndex = 0;

            for (int i = 0; i < numVertices; i++)
            {
                for (int j = i + 1; j < numVertices; j++)
                {
                    if (NumDifferingBits(i, j) == 1)
                    {
                        result[edgeIndex] = new int[] { i, j };
                        edgeIndex++;
                    }
                }
            }
            return result;
        }

        private int NumDifferingBits(int a, int b)
        {
            int diff = a ^ b;
            int count = 0;
            while (diff != 0)
            {
                count++;
                diff &= (diff - 1);
            }
            return count;
        }

        private int[][] ProjectVertices(double[][] vertices, double[][] rotationMatrix)
        {
            int numVertices = vertices.Length;
            int numDimensions = vertices[0].Length;
            int numProjectedDimensions = rotationMatrix[0].Length;

            int[][] projectedVertices = new int[numVertices][];

            for (int i = 0; i < numVertices; i++)
            {
                projectedVertices[i] = new int[numProjectedDimensions];
                for (int j = 0; j < numProjectedDimensions; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < numDimensions; k++)
                    {
                        sum += vertices[i][k] * rotationMatrix[k][j];
                    }
                    projectedVertices[i][j] = (int)Math.Round(sum);
                }
            }
            return projectedVertices;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = ClientSize.Width;
            int height = ClientSize.Height;
            int centerX = width / 2;
            int centerY = height / 2;

            // Plot the hypercube edges
            foreach (int[] edge in edges)
            {
                int x1 = (int)(centerX + vertices[edge[0]][0] * 50);
                int y1 = (int)(centerY + vertices[edge[0]][1] * 50);
                int x2 = (int)(centerX + vertices[edge[1]][0] * 50);
                int y2 = (int)(centerY + vertices[edge[1]][1] * 50);

                g.DrawLine(Pens.Black, x1, y1, x2, y2);
            }

            // Project vertices onto 2D space
            double[][] rotationMatrix3D = { { Math.Cos(angle), 0, -Math.Sin(angle) },
                { 0, Math.Cos(angle), 0 }, { Math.Sin(angle), 0, Math.Cos(angle) } };
            int[][] projectedVertices3D = ProjectVertices(vertices, rotationMatrix3D);

            double[][] rotationMatrix45678 = { { 1, 0, 0 }, { 0, Math.Cos(angle), -Math.Sin(angle) },
                { 0, Math.Sin(angle), Math.Cos(angle) } };
            int[][] projectedVertices45678 = ProjectVertices(projectedVertices3D, rotationMatrix45678);

            double[][] rotationMatrix9 = { { Math.Cos(angle), -Math.Sin(angle), 0 },
                { Math.Sin(angle), Math.Cos(angle), 0 }, { 0, 0, 1 } };
            int[][] projectedVertices9 = ProjectVertices(projectedVertices45678, rotationMatrix9);

            // Plot projected vertices with labels
            for (int i = 0; i < projectedVertices3D.Length; i++)
            {
                int x = (int)(centerX + projectedVertices3D[i][0] * 50);
                int y = (int)(centerY + projectedVertices3D[i][1] * 50);

                g.FillEllipse(Brushes.Black, x - 3, y - 3, 6, 6);
                g.DrawString(i.ToString(), Font, Brushes.Black, x - 3, y - 3);

                for (int j = i + 1; j < projectedVertices3D.Length; j++)
                {
                    int x1 = (int)(centerX + projectedVertices3D[i][0] * 50);
                    int y1 = (int)(centerY + projectedVertices3D[i][1] * 50);
                    int x2 = (int)(centerX + projectedVertices3D[j][0] * 50);
                    int y2 = (int)(centerY + projectedVertices3D[j][1] * 50);

                    g.DrawLine(Pens.Gray, x1, y1, x2, y2);
                }
            }
        }

        public HypercubePlotForm()
        {
            vertices = GenerateHypercubeVertices(numDimensions);
            edges = GenerateHypercubeEdges(numDimensions);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ClientSize = new Size(800, 800);
            BackColor = Color.White;
            Text = "Hypercube Plot";
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new HypercubePlotForm());
        }
    }
}
