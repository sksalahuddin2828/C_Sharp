using System;
using System.Collections.Generic;
using UnityEngine;

public class HypercubeVisualization : MonoBehaviour
{
    public int numDimensions = 8;
    public float angle = Mathf.PI / 4f;
    public float vertexSize = 0.1f;
    public float lineOpacity = 0.3f;

    private List<Vector3> vertices;
    private List<Edge> edges;
    private List<Vector3> projectedVertices3D;
    private List<Vector3> projectedVertices45678;
    private List<string> labels;

    private class Edge
    {
        public int vertexIndex1;
        public int vertexIndex2;

        public Edge(int index1, int index2)
        {
            vertexIndex1 = index1;
            vertexIndex2 = index2;
        }
    }

    private void Start()
    {
        GenerateHypercube();
        GenerateHypercubeEdges();
        ProjectVertices();
        PlotHypercube();
    }

    private void GenerateHypercube()
    {
        vertices = new List<Vector3>();

        int vertexCount = (int)Mathf.Pow(2, numDimensions);
        for (int i = 0; i < vertexCount; i++)
        {
            int[] binaryCoordinates = DecimalToBinary(i, numDimensions);
            Vector3 vertex = new Vector3();
            for (int j = 0; j < numDimensions; j++)
            {
                vertex[j] = binaryCoordinates[j] == 0 ? -1f : 1f;
            }
            vertices.Add(vertex);
        }
    }

    private int[] DecimalToBinary(int decimalNumber, int numDigits)
    {
        int[] binary = new int[numDigits];
        for (int i = numDigits - 1; i >= 0; i--)
        {
            binary[i] = decimalNumber % 2;
            decimalNumber /= 2;
        }
        return binary;
    }

    private void GenerateHypercubeEdges()
    {
        edges = new List<Edge>();

        for (int i = 0; i < vertices.Count; i++)
        {
            for (int j = i + 1; j < vertices.Count; j++)
            {
                int numDifferences = 0;
                for (int k = 0; k < numDimensions; k++)
                {
                    if (vertices[i][k] != vertices[j][k])
                    {
                        numDifferences++;
                    }
                }
                if (numDifferences == 1)
                {
                    edges.Add(new Edge(i, j));
                }
            }
        }
    }

    private void ProjectVertices()
    {
        projectedVertices3D = new List<Vector3>();
        projectedVertices45678 = new List<Vector3>();
        labels = new List<string>();

        Quaternion rotation3D = Quaternion.Euler(0f, angle * Mathf.Rad2Deg, 0f);
        Quaternion rotation45678 = Quaternion.Euler(angle * Mathf.Rad2Deg, 0f, 0f);

        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 vertex3D = rotation3D * vertices[i];
            projectedVertices3D.Add(vertex3D);

            Vector3 vertex45678 = rotation45678 * vertex3D;
            projectedVertices45678.Add(vertex45678);

            string label = "";
            for (int j = 0; j < numDimensions; j++)
            {
                label += vertices[i][j] == -1f ? "0" : "1";
            }
            labels.Add(label);
        }
    }

    private void PlotHypercube()
    {
        for (int i = 0; i < edges.Count; i++)
        {
            int vertexIndex1 = edges[i].vertexIndex1;
            int vertexIndex2 = edges[i].vertexIndex2;
            Vector3 vertex1 = projectedVertices3D[vertexIndex1];
            Vector3 vertex2 = projectedVertices3D[vertexIndex2];
            DrawLine(vertex1, vertex2, Color.black);
        }

        for (int i = 0; i < projectedVertices3D.Count; i++)
        {
            Vector3 vertex3D = projectedVertices3D[i];
            Vector3 vertex45678 = projectedVertices45678[i];
            string label = labels[i];
            DrawVertex(vertex3D, vertexSize, label);
            DrawLine(vertex3D, vertex45678, Color.black, lineOpacity);
        }
    }

    private void DrawVertex(Vector3 position, float size, string label)
    {
        GameObject vertex = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        vertex.transform.position = position;
        vertex.transform.localScale = new Vector3(size, size, size);
        vertex.GetComponent<Renderer>().material.color = Color.white;
        vertex.GetComponent<Collider>().enabled = false;

        GameObject labelObj = new GameObject();
        labelObj.transform.position = position;
        labelObj.transform.rotation = Camera.main.transform.rotation;
        labelObj.transform.localScale = Vector3.one * size * 2f;
        TextMesh textMesh = labelObj.AddComponent<TextMesh>();
        textMesh.text = label;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.fontSize = 14;
        textMesh.color = Color.black;
    }

    private void DrawLine(Vector3 start, Vector3 end, Color color, float opacity = 1f)
    {
        GameObject line = new GameObject();
        line.transform.position = start;
        line.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = new Color(color.r, color.g, color.b, opacity);
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
