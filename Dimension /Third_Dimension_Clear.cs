using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;

namespace HypercubePlot
{
    public class Game : GameWindow
    {
        private List<Edge> edges;
        private Vector3[] vertices;
        private Vector3[] projectedVertices;
        private Random random;
        private bool isRotating;
        private float rotationAngle;

        public Game() : base(800, 800, GraphicsMode.Default, "Hypercube Plot")
        {
            random = new Random();
            edges = GenerateHypercubeEdges(5);
            vertices = new Vector3[32];
            projectedVertices = new Vector3[32];
            isRotating = false;
            rotationAngle = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.ClearColor(Color.White);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 1, 64);
            GL.LoadMatrix(ref projection);
            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 modelview = Matrix4.LookAt(new Vector3(0, 0, -10), Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref modelview);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (Keyboard.GetState().IsKeyDown(Key.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Key.Space))
                isRotating = !isRotating;

            if (isRotating)
            {
                rotationAngle += 0.01f;
                if (rotationAngle >= MathHelper.TwoPi)
                    rotationAngle = 0;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.LoadIdentity();
            GL.Rotate(rotationAngle * MathHelper.RadToDeg, Vector3.UnitZ);

            GL.PointSize(8);
            GL.Color3(Color.Black);
            foreach (Edge edge in edges)
            {
                GL.Begin(PrimitiveType.Lines);
                for (int j = 0; j < 5; j++)
                {
                    int vertex = edge.Vertices[j];
                    Vector3 v = new Vector3();
                    for (int k = 0; k < 5; k++)
                    {
                        v += new Vector3(((vertex >> k) & 1) == 0 ? 1 : -1, 0, 0);
                    }
                    GL.Vertex3(v);
                }
                GL.End();
            }

            for (int i = 0; i < 32; i++)
            {
                Vector3 vertex = projectedVertices[i];
                GL.PushMatrix();
                GL.Translate(vertex);
                GL.Scale(0.1f, 0.1f, 0.1f);
                TextRenderer.DrawText(vertex.ToString(), new System.Drawing.Font("Arial", 8), System.Drawing.Color.Black, 0, 0);
                GL.PopMatrix();
            }

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 1, 64);
            GL.LoadMatrix(ref projection);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private List<Edge> GenerateHypercubeEdges(int dimensions)
        {
            int numVertices = (int)Math.Pow(2, dimensions);
            int[] grayCodes = new int[numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                grayCodes[i] = i ^ (i >> 1);
            }

            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    int neighbor = i ^ (1 << j);
                    if (grayCodes[i] < grayCodes[neighbor])
                    {
                        edges.Add(new Edge(i, neighbor));
                    }
                }
            }

            return edges;
        }

        private void GenerateHypercubeVertices()
        {
            for (int i = 0; i < 32; i++)
            {
                vertices[i] = new Vector3();
                for (int j = 0; j < 5; j++)
                {
                    vertices[i].X += ((i >> j) & 1) == 0 ? 1 : -1;
                }
            }
        }

        private void ApplyRotations()
        {
            for (int i = 0; i < 32; i++)
            {
                Vector3 vertex = vertices[i];
                projectedVertices[i] = vertex;
                for (int j = 3; j < 5; j++)
                {
                    projectedVertices[i] = Vector3.Transform(projectedVertices[i], Matrix4.CreateFromAxisAngle(Vector3.UnitZ, MathHelper.TwoPi / 5));
                }
            }
        }

        private void AddRandomOffsets()
        {
            for (int i = 0; i < 32; i++)
            {
                Vector3 offset = new Vector3((float)(random.NextDouble() * 0.4 - 0.2), (float)(random.NextDouble() * 0.4 - 0.2), (float)(random.NextDouble() * 0.4 - 0.2));
                projectedVertices[i] += offset;
            }
        }

        private class Edge
        {
            public int[] Vertices { get; }

            public Edge(int vertex1, int vertex2)
            {
                Vertices = new int[] { vertex1, vertex2 };
            }
        }

        [STAThread]
        public static void Main()
        {
            using (Game game = new Game())
            {
                game.GenerateHypercubeVertices();
                game.ApplyRotations();
                game.AddRandomOffsets();
                game.Run(60);
            }
        }
    }
}
