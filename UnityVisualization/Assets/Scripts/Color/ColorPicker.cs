using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public float radius;
    public float inCircleRadius;
    public ColorWheel wheel;
    public GameObject picker;
	public BoxPicker boxPicker;
    public Camera uiCamera;
    private int degree = 20;
    private bool isClicked = false;
    public Material material;
    public Material gradationMaterial;

    private float angle = 0;
    Ray ray;

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
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        }
        mesh.uv = uvs;
        GetComponent<MeshRenderer>().material = material;
        mesh.RecalculateNormals();
		gradationMaterial.color = Color.red;

	}

    void Update()
    {
        RaycastHit hit;
        ray = uiCamera.ScreenPointToRay(Input.mousePosition);

        if (isClicked)
        {
            var p = uiCamera.WorldToScreenPoint(picker.transform.position);
            var vector = Input.mousePosition - p;
            angle = Mathf.Atan2(vector.x, vector.y);
            float x = Mathf.Sin(angle) * (wheel.radius + wheel.inCircleRadius) / 2;
            float y = Mathf.Cos(angle) * (wheel.radius + wheel.inCircleRadius) / 2;
            this.transform.localPosition = new Vector3(x,y,-2);
            gradationMaterial.color = getColor();
			boxPicker.UpdateColor();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(Physics.Raycast(ray, out hit) && hit.transform.tag == "Picker")
                {
                    isClicked = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }

    Vector3 getCircularPosition(int currentDegree, float _radius)
    {
        float deg = (float)currentDegree / degree * 360.0f;
        float x = Mathf.Sin(deg * Mathf.PI / 180) * _radius;
        float y = Mathf.Cos(deg * Mathf.PI / 180) * _radius;
        return new Vector3(x, y, 0);
    }

    int[] linkCircles(Vector3[] _vertices)
    {
        var temp = new int[degree * 6];
        for (int i = 0; i < degree; i++)
        {
            temp[i * 3] = i;
            temp[i * 3 + 1] = (i + 1) % degree;
            temp[i * 3 + 2] = i + degree;
        }
        for (int i = degree; i < degree * 2; i++)
        {
            temp[i * 3] = i;
            temp[i * 3 + 1] = (i + 1) % degree;
            temp[i * 3 + 2] = degree + (i + 1) % degree;
        }
        return temp;
    }

    public Color getColor()
    {
        var c = Color.HSVToRGB(((angle + 2 * Mathf.PI) % (2 * Mathf.PI)) * Mathf.Rad2Deg / 360, 1, 1,true);
        return c;
    }
}
