using System;
using UnityEngine;

public class TesseractPlot : MonoBehaviour
{
    public GameObject pointPrefab;
    public Material lineMaterial;
    public Material textMaterial;
    public float rotationAngle = Mathf.PI / 4f;

    private Vector5[] vertices;
    private int[,] edges;

    private void Start()
    {
        // Define tesseract vertices
        vertices = new Vector5[]
        {
            new Vector5(-1, -1, -1, -1, -1),
            new Vector5(-1, -1, -1, -1,  1),
            new Vector5(-1, -1, -1,  1, -1),
            new Vector5(-1, -1, -1,  1,  1),
            new Vector5(-1, -1,  1, -1, -1),
            new Vector5(-1, -1,  1, -1,  1),
            new Vector5(-1, -1,  1,  1, -1),
            new Vector5(-1, -1,  1,  1,  1),
            new Vector5(-1,  1, -1, -1, -1),
            new Vector5(-1,  1, -1, -1,  1),
            new Vector5(-1,  1, -1,  1, -1),
            new Vector5(-1,  1, -1,  1,  1),
            new Vector5(-1,  1,  1, -1, -1),
            new Vector5(-1,  1,  1, -1,  1),
            new Vector5(-1,  1,  1,  1, -1),
            new Vector5(-1,  1,  1,  1,  1),
            new Vector5( 1, -1, -1, -1, -1),
            new Vector5( 1, -1, -1, -1,  1),
            new Vector5( 1, -1, -1,  1, -1),
            new Vector5( 1, -1, -1,  1,  1),
            new Vector5( 1, -1,  1, -1, -1),
            new Vector5( 1, -1,  1, -1,  1),
            new Vector5( 1, -1,  1,  1, -1),
            new Vector5( 1, -1,  1,  1,  1),
            new Vector5( 1,  1, -1, -1, -1),
            new Vector5( 1,  1, -1, -1,  1),
            new Vector5( 1,  1, -1,  1, -1),
            new Vector5( 1,  1, -1,  1,  1),
            new Vector5( 1,  1,  1, -1, -1),
            new Vector5( 1,  1,  1, -1,  1),
            new Vector5( 1,  1,  1,  1, -1),
            new Vector5( 1,  1,  1,  1,  1)
        };

        // Define edges of the tesseract
        edges = new int[,]
        {
            { 0, 1 }, { 0, 2 }, { 0, 4 }, { 1, 3 }, { 1, 5 }, { 2, 3 }, { 2, 6 }, { 3, 7 },
            { 4, 5 }, { 4, 6 }, { 5, 7 }, { 6, 7 }, { 8, 9 }, { 8, 10 }, { 8, 12 }, { 9, 11 },
            { 9, 13 }, { 10, 11 }, { 10, 14 }, { 11, 15 }, { 12, 13 }, { 12, 14 }, { 13, 15 },
            { 14, 15 }, { 0, 8 }, { 1, 9 }, { 2, 10 }, { 3, 11 }, { 4, 12 }, { 5, 13 }, { 6, 14 },
            { 7, 15 }
        };

        // Create the tesseract
        CreateTesseract();
    }

    private void CreateTesseract()
    {
        // Create the points
        for (int i = 0; i < vertices.Length; i++)
        {
            GameObject point = Instantiate(pointPrefab, vertices[i].ToVector3(), Quaternion.identity);
            point.transform.localScale = Vector3.one * 0.1f;
            point.GetComponent<Renderer>().material.color = new Color(vertices[i].v4, 0, 1 - vertices[i].v4);
            point.transform.SetParent(transform);
        }

        // Create the lines
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            int vertexIndex1 = edges[i, 0];
            int vertexIndex2 = edges[i, 1];
            Vector3 position1 = vertices[vertexIndex1].ToVector3();
            Vector3 position2 = vertices[vertexIndex2].ToVector3();

            GameObject line = new GameObject("Line");
            line.transform.SetParent(transform);
            line.AddComponent<LineRenderer>();
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, position1);
            lineRenderer.SetPosition(1, position2);
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = lineMaterial;
        }

        // Create the labels
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 position = vertices[i].ToVector3();

            GameObject label = new GameObject("Label");
            label.transform.SetParent(transform);
            TextMesh textMesh = label.AddComponent<TextMesh>();
            textMesh.text = vertices[i].ToString();
            textMesh.fontSize = 8;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.alignment = TextAlignment.Center;
            textMesh.characterSize = 0.1f;
            textMesh.color = Color.white;
            label.transform.position = position;
            label.transform.LookAt(Camera.main.transform.position);
            label.transform.Rotate(0, 180, 0);
        }
    }

    private void Update()
    {
        // Rotate the tesseract
        Quaternion rotation = Quaternion.Euler(0, rotationAngle * Mathf.Rad2Deg, 0);
        transform.rotation = rotation;
    }
}

public struct Vector5
{
    public float v1, v2, v3, v4, v5;

    public Vector5(float v1, float v2, float v3, float v4, float v5)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
        this.v4 = v4;
        this.v5 = v5;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(v1, v2, v3);
    }

    public override string ToString()
    {
        return $"({v1}, {v2}, {v3}, {v4}, {v5})";
    }
}
