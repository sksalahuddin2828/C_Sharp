using System.Collections.Generic;
using UnityEngine;

public class TesseractProjection : MonoBehaviour
{
    // Define the vertices of a unit tesseract in 4D space
    private Vector4[] vertices = new Vector4[]
    {
        new Vector4(-0.5f, -0.5f, -0.5f, -0.5f),
        new Vector4(0.5f, -0.5f, -0.5f, -0.5f),
        new Vector4(0.5f, 0.5f, -0.5f, -0.5f),
        new Vector4(-0.5f, 0.5f, -0.5f, -0.5f),
        new Vector4(-0.5f, -0.5f, 0.5f, -0.5f),
        new Vector4(0.5f, -0.5f, 0.5f, -0.5f),
        new Vector4(0.5f, 0.5f, 0.5f, -0.5f),
        new Vector4(-0.5f, 0.5f, 0.5f, -0.5f),
        new Vector4(-0.5f, -0.5f, -0.5f, 0.5f),
        new Vector4(0.5f, -0.5f, -0.5f, 0.5f),
        new Vector4(0.5f, 0.5f, -0.5f, 0.5f),
        new Vector4(-0.5f, 0.5f, -0.5f, 0.5f),
        new Vector4(-0.5f, -0.5f, 0.5f, 0.5f),
        new Vector4(0.5f, -0.5f, 0.5f, 0.5f),
        new Vector4(0.5f, 0.5f, 0.5f, 0.5f),
        new Vector4(-0.5f, 0.5f, 0.5f, 0.5f),
    };

    // Define the edges of the tesseract
    private int[][] edges = new int[][]
    {
        new int[] {0, 1}, new int[] {1, 2}, new int[] {2, 3}, new int[] {3, 0},
        new int[] {4, 5}, new int[] {5, 6}, new int[] {6, 7}, new int[] {7, 4},
        new int[] {0, 4}, new int[] {1, 5}, new int[] {2, 6}, new int[] {3, 7},
        new int[] {8, 9}, new int[] {9, 10}, new int[] {10, 11}, new int[] {11, 8},
        new int[] {12, 13}, new int[] {13, 14}, new int[] {14, 15}, new int[] {15, 12},
        new int[] {8, 12}, new int[] {9, 13}, new int[] {10, 14}, new int[] {11, 15},
        new int[] {0, 8}, new int[] {1, 9}, new int[] {2, 10}, new int[] {3, 11},
        new int[] {4, 12}, new int[] {5, 13}, new int[] {6, 14}, new int[] {7, 15},
    };

    void Start()
    {
        // Project the tesseract's vertices to 3D space
        Vector3[] projection3D = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            projection3D[i] = new Vector3((vertices[i].x + vertices[i].y) / 2.0f,
                                          (vertices[i].z + vertices[i].w) / 2.0f,
                                          (vertices[i].x + vertices[i].z) / 2.0f);
        }

        // Draw the edges
        for (int i = 0; i < edges.Length; i++)
        {
            Vector3 p1 = projection3D[edges[i][0]] * 2.0f;
            Vector3 p2 = projection3D[edges[i][1]] * 2.0f;
            Debug.DrawLine(p1, p2, Color.blue);
        }

        // Draw the faces of the tesseract
        int[][] faces = new int[][] {
            new int[] { 0, 1, 2, 3 }, new int[] { 4, 5, 6, 7 }, new int[] { 0, 1, 5, 4 },
            new int[] { 2, 3, 7, 6 }, new int[] { 0, 3, 7, 4 }, new int[] { 1, 2, 6, 5 },
            new int[] { 8, 9, 10, 11 }, new int[] { 12, 13, 14, 15 }, new int[] { 8, 9, 13, 12 },
            new int[] { 10, 11, 15, 14 }, new int[] { 8, 11, 15, 12 }, new int[] { 9, 10, 14, 13 },
            new int[] { 0, 8, 12, 4 }, new int[] { 1, 9, 13, 5 }, new int[] { 2, 10, 14, 6 }, new int[] { 3, 11, 15, 7 }
        };
        for (int i = 0; i < faces.Length; i++)
        {
            Vector3[] faceVertices = new Vector3[faces[i].Length];
            for (int j = 0; j < faces[i].Length; j++)
            {
                faceVertices[j] = projection3D[faces[i][j]] * 2.0f;
            }
            DrawPolygon(faceVertices, Color.cyan, Color.red);
        }
    }

    // Function to draw a polygon in 3D space
    private void DrawPolygon(Vector3[] vertices, Color faceColor, Color edgeColor)
    {
        GameObject polygonObject = new GameObject("Polygon");
        MeshRenderer meshRenderer = polygonObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = polygonObject.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;

        int[] triangles = new int[(vertices.Length - 2) * 3];
        int count = 0;
        for (int i = 2; i < vertices.Length; i++)
        {
            triangles[count] = 0;
            triangles[count + 1] = i - 1;
            triangles[count + 2] = i;
            count += 3;
        }
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        Material material = new Material(Shader.Find("Standard"));
        material.color = faceColor;
        meshRenderer.material = material;

        polygonObject.transform.SetParent(transform);

        for (int i = 0; i < vertices.Length; i++)
        {
            Debug.DrawLine(vertices[i], vertices[(i + 1) % vertices.Length], edgeColor);
        }
    }
}
