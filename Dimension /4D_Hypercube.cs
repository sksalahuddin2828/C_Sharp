using System;
using UnityEngine;

public class TesseractProjection : MonoBehaviour
{
    // Define tesseract vertices
    private Vector4[] vertices = new Vector4[]
    {
        new Vector4(-1, -1, -1, -1),
        new Vector4(-1, -1, -1, 1),
        new Vector4(-1, -1, 1, -1),
        new Vector4(-1, -1, 1, 1),
        new Vector4(-1, 1, -1, -1),
        new Vector4(-1, 1, -1, 1),
        new Vector4(-1, 1, 1, -1),
        new Vector4(-1, 1, 1, 1),
        new Vector4(1, -1, -1, -1),
        new Vector4(1, -1, -1, 1),
        new Vector4(1, -1, 1, -1),
        new Vector4(1, -1, 1, 1),
        new Vector4(1, 1, -1, -1),
        new Vector4(1, 1, -1, 1),
        new Vector4(1, 1, 1, -1),
        new Vector4(1, 1, 1, 1),
        new Vector4(1, 1, 1, 1) // Second dimension vertex
    };

    // Define edges of the tesseract
    private int[,] edges = new int[,]
    {
        { 0, 1 }, { 0, 2 }, { 0, 4 }, { 1, 3 }, { 1, 5 }, { 2, 3 }, { 2, 6 }, { 3, 7 },
        { 4, 5 }, { 4, 6 }, { 5, 7 }, { 6, 7 }, { 8, 9 }, { 8, 10 }, { 8, 12 }, { 9, 11 },
        { 9, 13 }, { 10, 11 }, { 10, 14 }, { 11, 15 }, { 12, 13 }, { 12, 14 }, { 13, 15 },
        { 14, 15 }, { 0, 8 }, { 1, 9 }, { 2, 10 }, { 3, 11 }, { 4, 12 }, { 5, 13 }, { 6, 14 },
        { 7, 15 }
    };

    // Start is called before the first frame update
    void Start()
    {
        // Create a GameObject for the tesseract
        GameObject tesseract = new GameObject("Tesseract");

        // Create a LineRenderer component for the tesseract edges
        LineRenderer lineRenderer = tesseract.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;

        // Project vertices onto 3D space (select the first three components)
        Vector3[] projectedVertices = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            projectedVertices[i] = new Vector3(vertices[i].x, vertices[i].y, vertices[i].z);
        }

        // Set the positions for the tesseract edges
        lineRenderer.positionCount = edges.GetLength(0) * 2;
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            lineRenderer.SetPosition(i * 2, projectedVertices[edges[i, 0]]);
            lineRenderer.SetPosition(i * 2 + 1, projectedVertices[edges[i, 1]]);
        }
    }
}
