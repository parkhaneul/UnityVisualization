using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAPI : MonoBehaviour {

    public Material material;
    public bool semi_permeable = true;
    public Vector3 roomSize = new Vector3(1, 1, 1);
    public Vector3 doorSize = new Vector3(1, 1, 1);

    private Vector3[] vertices;

    private int[] triangle = new int[]{
        2,1,5, //bottm
        6,2,5,
        3,0,1, //back
        2,3,1,
        7,3,6, //right
        6,3,2,
        4,0,3, //top
        4,3,7,
        5,4,7, //front
        5,7,6,
        0,4,5, //left
        1,0,5
    };

    private Mesh mesh;

    // Use this for initialization
    void Start () {
        mesh = new Mesh();

        vertices = new Vector3[]
        {
            new Vector3(roomSize.x * -1, roomSize.y * 1, roomSize.y * -1),
            new Vector3(roomSize.x * -1, roomSize.y * -1, roomSize.y * -1),
            new Vector3(roomSize.x * 1, roomSize.y * -1, roomSize.y * -1),
            new Vector3(roomSize.x * 1, roomSize.y * 1, roomSize.y * -1),
            new Vector3(roomSize.x * -1, roomSize.y * 1, roomSize.y * 1),
            new Vector3(roomSize.x * -1, roomSize.y * -1, roomSize.y * 1),
            new Vector3(roomSize.x * 1, roomSize.y * -1, roomSize.y * 1),
            new Vector3(roomSize.x * 1, roomSize.y * 1, roomSize.y * 1),
        };

        mesh.vertices = vertices;
        mesh.triangles = triangle;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = material;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
