// C# using Helix Toolkit (WPF)

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Tesseract3DProjection
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Define tesseract vertices
            double[,] vertices = new double[,]
            {
                {-1, -1, -1, -1, -1},
                {-1, -1, -1, -1, 1},
                {-1, -1, -1, 1, -1},
                {-1, -1, -1, 1, 1},
                {-1, -1, 1, -1, -1},
                {-1, -1, 1, -1, 1},
                {-1, -1, 1, 1, -1},
                {-1, -1, 1, 1, 1},
                {-1, 1, -1, -1, -1},
                {-1, 1, -1, -1, 1},
                {-1, 1, -1, 1, -1},
                {-1, 1, -1, 1, 1},
                {-1, 1, 1, -1, -1},
                {-1, 1, 1, -1, 1},
                {-1, 1, 1, 1, -1},
                {-1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1} // Second dimension vertex
            };

            // Define edges of the tesseract
            int[,] edges = new int[,]
            {
                {0, 1}, {0, 2}, {0, 4}, {1, 3}, {1, 5}, {2, 3}, {2, 6}, {3, 7},
                {4, 5}, {4, 6}, {5, 7}, {6, 7}, {8, 9}, {8, 10}, {8, 12}, {9, 11},
                {9, 13}, {10, 11}, {10, 14}, {11, 15}, {12, 13}, {12, 14}, {13, 15},
                {14, 15}, {0, 8}, {1, 9}, {2, 10}, {3, 11}, {4, 12}, {5, 13}, {6, 14},
                {7, 15}
            };

            // Create a new HelixViewport3D
            var viewport3D = new HelixViewport3D();
            viewport3D.IsRotationEnabled = false;

            // Create a new ModelVisual3D
            var model = new ModelVisual3D();

            // Project vertices onto 3D space (select the first three components)
            var projectedVertices = new Point3DCollection();
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                projectedVertices.Add(new Point3D(vertices[i, 0], vertices[i, 1], vertices[i, 2]));
            }

            // Add points to the viewport
            foreach (var vertex in projectedVertices)
            {
                var point = new PointsVisual3D
                {
                    Points = new Point3DCollection { vertex },
                    Color = Colors.Black,
                    Size = 3
                };
                model.Children.Add(point);
            }

            // Add lines to the viewport
            foreach (var edge in edges)
            {
                var line = new LinesVisual3D
                {
                    Points = new Point3DCollection
                    {
                        projectedVertices[edge[0]],
                        projectedVertices[edge[1]]
                    },
                    Color = Colors.Gray,
                    Thickness = 1,
                    DashPattern = new double[] { 2, 2 }
                };
                model.Children.Add(line);
            }

            // Add the model to the viewport
            viewport3D.Children.Add(model);

            // Add the viewport to the main window
            MainGrid.Children.Add(viewport3D);
        }
    }
}
