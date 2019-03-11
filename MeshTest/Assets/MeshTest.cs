using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
	Vector3[] newVertices = new Vector3[9];
	int[] newTriangles = new int[]
	{
	  0,1,2,
	  0,2,3,
	  0,3,4,
	  0,4,5,
	  0,5,6,
	  0,6,7,
	  0,7,8,
	  0,8,1
	};

	int size = 3;

	// Start is called before the first frame update
	void Start()
	{
		newVertices[0] = new Vector3(0, 0);
		newVertices[1] = new Vector3(size, 0);
		newVertices[2] = new Vector3(Mathf.Cos(45 * Mathf.Deg2Rad) * size, Mathf.Cos(45 * Mathf.Deg2Rad) * size);
		newVertices[3] = new Vector3(0, size);
		newVertices[4] = new Vector3(-Mathf.Cos(45 * Mathf.Deg2Rad) * size, Mathf.Cos(45 * Mathf.Deg2Rad) * size);
		newVertices[5] = new Vector3(-size, 0);
		newVertices[6] = new Vector3(-Mathf.Cos(45 * Mathf.Deg2Rad) * size, -Mathf.Cos(45 * Mathf.Deg2Rad) * size);
		newVertices[7] = new Vector3(0, -size);
		newVertices[8] = new Vector3(Mathf.Cos(45 * Mathf.Deg2Rad) * size, -Mathf.Cos(45 * Mathf.Deg2Rad) * size);

		Mesh mesh = new Mesh();
		mesh.vertices = newVertices;
		mesh.triangles = newTriangles;
		GetComponent<MeshFilter>().mesh = mesh;
		//GetComponent<MeshRenderer>().
	}

	// Update is called once per frame
	void Update()
	{

	}
}