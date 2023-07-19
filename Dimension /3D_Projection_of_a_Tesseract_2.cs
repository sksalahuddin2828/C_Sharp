using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace TesseractProjection
{
    public partial class Form1 : Form
    {
        private List<PointF> vertices;
        private List<Tuple<int, int>> edges;
        private List<List<int>> projectedEdges;

        public Form1()
        {
            InitializeComponent();

            // Define tesseract vertices
            vertices = new List<PointF>
            {
                new PointF(-1, -1),
                new PointF(-1, 1),
                new PointF(1, -1),
                new PointF(1, 1)
            };

            // Define edges of the tesseract
            edges = new List<Tuple<int, int>>
            {
                Tuple.Create(0, 1),
                Tuple.Create(0, 2),
                Tuple.Create(1, 3),
                Tuple.Create(2, 3)
            };

            // Create a projection matrix to project 4D points onto 2D space
            float[,] projectionMatrix = new float[2, 4]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 }
            };

            // Project vertices onto 2D space
            List<PointF> projectedVertices = ProjectVertices(vertices, projectionMatrix);

            // Create projected edges
            projectedEdges = new List<List<int>>();
            foreach (var edge in edges)
            {
                int vertex1 = edge.Item1;
                int vertex2 = edge.Item2;
                List<int> projectedEdge = new List<int>
                {
                    vertex1,
                    vertex2
                };
                projectedEdges.Add(projectedEdge);
            }

            // Set the size of the form
            Size = new Size(400, 400);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Plot the tesseract edges
            foreach (var edge in projectedEdges)
            {
                PointF vertex1 = vertices[edge[0]];
                PointF vertex2 = vertices[edge[1]];
                g.DrawLine(Pens.Gray, vertex1, vertex2);
            }

            // Plot projected vertices with labels
            for (int i = 0; i < vertices.Count; i++)
            {
                PointF vertex = vertices[i];
                g.FillEllipse(Brushes.Red, vertex.X - 3, vertex.Y - 3, 6, 6);
                g.DrawString(i.ToString(), Font, Brushes.Black, vertex);
            }

            // Add a title and description
            g.DrawString("2D Projection of a Tesseract (4D Hypercube)", Font, Brushes.Black, new PointF(10, 10));
            g.DrawString("A tesseract is a 4D hypercube represented in 2D space using projection.", Font, Brushes.Black, new PointF(10, 30));
            g.DrawString("The color coding represents the 5th dimension.", Font, Brushes.Black, new PointF(10, 50));
        }

        private List<PointF> ProjectVertices(List<PointF> vertices, float[,] projectionMatrix)
        {
            List<PointF> projectedVertices = new List<PointF>();

            foreach (var vertex in vertices)
            {
                float x = projectionMatrix[0, 0] * vertex.X + projectionMatrix[0, 1] * vertex.Y + projectionMatrix[0, 2] * vertex.Z + projectionMatrix[0, 3] * vertex.W;
                float y = projectionMatrix[1, 0] * vertex.X + projectionMatrix[1, 1] * vertex.Y + projectionMatrix[1, 2] * vertex.Z + projectionMatrix[1, 3] * vertex.W;

                projectedVertices.Add(new PointF(x, y));
            }

            return projectedVertices;
        }
    }
}
