using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public float radius;
    public float inCircleRadius;
    [Range(3, 60)]
    public int degree = 3;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        var vertices = new Vector3[degree * 2];
        for (int i = 0; i < degree; i++)
        {
            vertices[i] = getCircularPosition(i, radius);
        }
        for (int i = 0; i < degree; i++)
        {
            vertices[i + degree] = getCircularPosition(i, inCircleRadius);
        }
        mesh.vertices = vertices;
        mesh.triangles = linkCircles(vertices);
        mesh.RecalculateNormals();
    }

    Vector3 getCircularPosition(int currentDegree,float _radius)
    {
        float deg = (float)currentDegree / degree * 360.0f;
        float x = Mathf.Sin(deg * Mathf.PI / 180) * _radius;
        float y = Mathf.Cos(deg * Mathf.PI / 180) * _radius;
        return new Vector3(x, y, 0);
    }

    int[] linkCircles(Vector3[] _vertices)
    {
        var temp = new int[degree * 6];
        for(int i = 0; i < degree; i++)
        {
            temp[i * 3] = i;
            temp[i * 3 + 1] = (i + 1) % degree;
            temp[i * 3 + 2] = i + degree;
            /*
             *  0 - 0 1 10
             *  1 - 1 2 11
             *  2 - 2 3 12
             *  3 - 3 4 13
             *  4 - 4 5 14
             *  5 - 5 6 15
             *  6 - 6 7 16
             *  7 - 7 8 17
             *  8 - 8 9 18
             *  9 - 9 0 19  
             */           
        }
        for(int i = degree; i < degree*2; i++)
        {
            temp[i * 3] = i;
            temp[i * 3 + 1] = (i+1) % degree;
            temp[i * 3 + 2] = degree + (i+1)%degree;
            /*
            *  10 - 10 1 11
            *  11 - 11 2 12
            *  12 - 12 3 13
            *  13 - 13 4 14
            *  14 - 14 5 15
            *  15 - 15 6 16
            *  16 - 16 7 17
            *  17 - 17 8 18
            *  18 - 18 9 19
            *  19 - 19 0 10  
            */
        }
        return temp;
    }
}
