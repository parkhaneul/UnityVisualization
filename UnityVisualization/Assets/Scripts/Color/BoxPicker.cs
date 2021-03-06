﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPicker : MonoBehaviour
{
    public float radius;
    public float inCircleRadius;
    public GameObject gradationBox;
    public GameObject picker;
    public Camera uiCamera;
    private int degree = 20;
    private bool isClicked = false;
    public Material material;
    public ColorPicker ColorPicker;
	public static Color nowColor;
    Ray ray;

	private float preX;
	private float preY;
    // Start is called before the first frame update
    void Start()
    {
		this.transform.localPosition = new Vector3(-4, 3, 4);
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
    }

    void Update()
    {
        RaycastHit hit;
        ray = uiCamera.ScreenPointToRay(Input.mousePosition);
        if (isClicked)
        {
            var p = uiCamera.WorldToScreenPoint(picker.transform.position);
            var vector = (Input.mousePosition - p);
            var xScale = gradationBox.transform.lossyScale.x;
			var yScale = gradationBox.transform.lossyScale.z;
			preX = Mathf.Clamp(vector.x, -xScale * 4, xScale * 4);
			preY = Mathf.Clamp(vector.y, -yScale * 4, yScale * 4);
            this.transform.position = gradationBox.transform.position + new Vector3(preX, preY, -2);

			UpdateColor();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "BoxPicker")
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

	public void UpdateColor()
	{
		var color = ColorPicker.getColor();
		float h, s, v;
		Color.RGBToHSV(color, out h, out s, out v);
		var c = Color.HSVToRGB(h, (preX + 16) / 32, (preY + 16) / 32, true);

		nowColor = c;
		AxisViewManager.Instance().changeColor(nowColor);
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
	
}