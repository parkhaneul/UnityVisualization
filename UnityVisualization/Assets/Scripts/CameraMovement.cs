using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //public GameObject Target;
    public Vector3 Target = Vector3.zero;
    public float CamSpeed;
	public float zoomSpeed;
	public Transform defaultTransform;
	private float camDistanceToTarget;
	private Vector3 lastPosition;

	private void Start()
	{
		camDistanceToTarget = Vector3.Distance(this.transform.position, Target);
		defaultTransform = this.transform;
	}

	private void LateUpdate()
	{
		if (Input.GetKey(KeyCode.LeftAlt))
		{
			if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				Debug.Log(camDistanceToTarget);
				if (camDistanceToTarget < 150)
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

			if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2))||Input.GetMouseButtonDown(1))
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
			else if (Input.GetMouseButton(1))
			{
				Transform temp = this.transform;
				temp.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * CamSpeed * 10, Input.GetAxis("Mouse X") * CamSpeed *10, 0));
				float X = temp.rotation.eulerAngles.x;
				float Y = temp.rotation.eulerAngles.y;
				transform.rotation = Quaternion.Euler(X, Y, 0);
			}
			else if (Input.GetMouseButton(2))
			{
				Vector3 deltaMousePosition = Input.mousePosition - lastPosition;
				this.transform.position -= (this.transform.right * deltaMousePosition.x + this.transform.up * deltaMousePosition.y) * Time.deltaTime * CamSpeed;
				camDistanceToTarget = Vector3.Distance(this.transform.position, Target);
			}
			
		}
		else if (Input.GetKeyDown(KeyCode.R))
		{
			ReturnToDefault();
		}

	}

	void ReturnToDefault()
	{
		this.transform.rotation = defaultTransform.rotation;
		this.transform.position = defaultTransform.position;
		camDistanceToTarget = Vector3.Distance(defaultTransform.position , Target);
	}
}
