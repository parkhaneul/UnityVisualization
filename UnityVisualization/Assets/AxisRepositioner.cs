using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRepositioner : MonoBehaviour
{
    public Camera view;
    public float value = 1;
    public LineRenderer myRenderer;
    public Collider sphereCollider;
    private bool isClicked = false;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = view.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 9;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.transform.tag == "axis")
        {
            if (Input.GetMouseButtonDown(0))
            {
                isClicked = true;
            }
        }

        if (isClicked && Physics.Raycast(ray, out hit) && hit.transform.tag == "axis_sphere")
        {
            Vector3 temp1, temp2;
            temp1 = hit.point;
            if(Physics.Raycast(temp1,view.transform.forward, out hit))
            {
                temp2 = hit.point;
                if (Vector3.Distance(transform.position, temp1) > Vector3.Distance(transform.position, temp2))
                {
                    transform.position = temp2;
                }
                else
                {
                    transform.position = temp1;
                }
            }
            else
            {
                transform.position = temp1;
            }
        }

        if (isClicked)
        {
            myRenderer.SetPosition(1, transform.position);
            if (Input.GetMouseButton(0))
            {
                isClicked = false;
            }
        }
    }
}
