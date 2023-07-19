using UnityEngine;

public class HypercubeGenerator : MonoBehaviour
{
    public int numDimensions = 8;
    public float rotationAngle = Mathf.PI / 4;

    private void Start()
    {
        // Generate hypercube vertices
        Vector3[] vertices = GenerateHypercubeVertices(numDimensions);

        // Define edges of the hypercube
        int numVertices = vertices.Length;
        List<(int, int)> edges = new List<(int, int)>();
        for (int i = 0; i < numVertices; i++)
        {
            for (int j = i + 1; j < numVertices; j++)
            {
                if (CountDifferingComponents(vertices[i], vertices[j]) == 1)
                {
                    edges.Add((i, j));
                }
            }
        }

        // Create GameObjects for the hypercube edges
        foreach ((int i, int j) in edges)
        {
            GameObject edgeObj = new GameObject("Edge");
            LineRenderer lineRenderer = edgeObj.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] { vertices[i], vertices[j] });
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.black;
            lineRenderer.endColor = Color.black;
        }

        // Define rotation matrix for the first three dimensions
        Matrix4x4 rotationMatrix3D = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, rotationAngle * Mathf.Rad2Deg, 0), Vector3.one);

        // Project vertices onto 3D space
        Vector3[] projectedVertices3D = new Vector3[numVertices];
        for (int i = 0; i < numVertices; i++)
        {
            projectedVertices3D[i] = rotationMatrix3D.MultiplyPoint3x4(vertices[i]);
        }

        // Define rotation matrix for the fourth to eighth dimensions
        Matrix4x4 rotationMatrix45678 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(rotationAngle * Mathf.Rad2Deg, 0, 0), Vector3.one);

        // Project vertices from 3D space to the fourth to eighth dimensions
        Vector3[] projectedVertices45678 = new Vector3[numVertices];
        for (int i = 0; i < numVertices; i++)
        {
            projectedVertices45678[i] = rotationMatrix45678.MultiplyPoint3x4(projectedVertices3D[i]);
        }

        // Create GameObjects for the projected vertices with labels
        for (int i = 0; i < numVertices; i++)
        {
            GameObject vertexObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            vertexObj.transform.position = projectedVertices3D[i];
            vertexObj.transform.localScale = Vector3.one * 0.2f;

            TextMesh labelMesh = vertexObj.AddComponent<TextMesh>();
            labelMesh.text = GetLabel(vertices[i]);
            labelMesh.characterSize = 0.1f;
            labelMesh.fontSize = 20;
            labelMesh.anchor = TextAnchor.MiddleCenter;
            labelMesh.alignment = TextAlignment.Center;
            labelMesh.color = Color.black;

            MeshRenderer sphereRenderer = vertexObj.GetComponent<MeshRenderer>();
            sphereRenderer.material = new Material(Shader.Find("Standard"));
            sphereRenderer.material.color = projectedVertices45678[i].z < 0 ? Color.red : Color.green;
        }

        // Create illusion lines connecting projected vertices in 3D space
        for (int i = 0; i < numVertices; i++)
        {
            for (int j = i + 1; j < numVertices; j++)
            {
                Debug.DrawLine(projectedVertices3D[i], projectedVertices3D[j], Color.black, 0.3f);
            }
        }
    }

    private Vector3[] GenerateHypercubeVertices(int dimensions)
    {
        int numVertices = (int)Mathf.Pow(2, dimensions);
        Vector3[] vertices = new Vector3[numVertices];
        for (int i = 0; i < numVertices; i++)
        {
            int[] components = GetComponents(i, dimensions);
            vertices[i] = new Vector3(components[0], components[1], components[2]);
        }
        return vertices;
    }

    private int[] GetComponents(int index, int dimensions)
    {
        int[] components = new int[dimensions];
        for (int i = 0; i < dimensions; i++)
        {
            components[i] = (index >> i) & 1;
        }
        return components;
    }

    private int CountDifferingComponents(Vector3 a, Vector3 b)
    {
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            if (a[i] != b[i])
            {
                count++;
            }
        }
        return count;
    }

    private string GetLabel(Vector3 vertex)
    {
        string label = "";
        for (int i = 0; i < 3; i++)
        {
            label += vertex[i].ToString();
        }
        return label;
    }
}
