using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradationBox : MonoBehaviour
{
    public float BoxSize;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        var vertices = new Vector3[4];
        vertices[0] = new Vector3(-1, 1, 0) * BoxSize/2;
        vertices[1] = new Vector3(1, 1, 0) * BoxSize / 2;
        vertices[2] = new Vector3(1, -1, 0) * BoxSize / 2;
        vertices[3] = new Vector3(-1, -1, 0) * BoxSize / 2;
        mesh.vertices = vertices;
        int[] tri = { 0, 1, 2, 2, 3, 0 };
        mesh.triangles = tri;
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        }
        mesh.uv = uvs;
        GetComponent<MeshRenderer>().material = material;
        mesh.RecalculateNormals();
    }
}
