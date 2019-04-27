using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{   
    private Vector3 lastPosition;
    public int distance = 0;
     // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = Input.mousePosition;
            }else if (Input.GetMouseButton(0))
            {
                var temp = lastPosition - Input.mousePosition;
                transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), (360.0f / Screen.width) * temp.normalized.x * 2);
                transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), (360.0f / Screen.height) * temp.normalized.y * 2);
                transform.LookAt(Vector3.zero);
            }
        }
    }
}
