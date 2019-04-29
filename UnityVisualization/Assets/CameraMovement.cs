using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //public GameObject Target;
    public Vector3 Target = Vector3.zero;
    public float CamSpeed;
	public float zoomSpeed;
	private float camDistanceToTarget;
	private Vector3 lastPosition;

	private void Start()
	{
		camDistanceToTarget = Vector3.Distance(this.transform.position, Target/*.transform.position*/);
	}

	private void LateUpdate()
	{
		if (Input.GetKey(KeyCode.LeftAlt))
		{
			if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				if (camDistanceToTarget < 15)
				{
					camDistanceToTarget += zoomSpeed;
					Vector3 dir = (this.transform.position - Target/*.transform.position*/).normalized;
					transform.position = dir * camDistanceToTarget;
				}

			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0)
			{
				if (camDistanceToTarget > 5)
				{
					camDistanceToTarget -= zoomSpeed;
					Vector3 dir = (this.transform.position - Target/*.transform.position*/).normalized;
					transform.position = dir * camDistanceToTarget;
				}
			}

			if (Input.GetMouseButtonDown(0))
			{
				lastPosition = Input.mousePosition;
			}
			else if (Input.GetMouseButton(0))
			{
				Vector3 deltaMousePosition = Input.mousePosition - lastPosition;
				Vector3 temp = new Vector3(0,0,0);
				temp = this.transform.position + (deltaMousePosition.x * -this.transform.right
					+ deltaMousePosition.y * -this.transform.up) * CamSpeed;

				Vector3 dir = (temp - Target/*.transform.position*/).normalized;

				transform.position = dir * camDistanceToTarget;

				transform.LookAt(Target/*.transform.position*/);
				lastPosition = Input.mousePosition;
			}
		}
	}
	



	/*
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
	*/
}
