using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRepositioner : MonoBehaviour
{
    public Camera view;
    public int index = 1;
    public LineRenderer myRenderer;
    private bool isClicked = false;

    Ray ray;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (index == SetDropDown.currentIndex)
        {
            RaycastHit hit;
            RaycastHit[] hits;
            ray = view.ScreenPointToRay(Input.mousePosition);
            int layerMask_sphere = 1 << 9;
            int layerMask_axis = 1 << 10;

            if (isClicked)
            {
                if (Vector3.Dot(view.transform.forward, transform.position) >= 0)
                {
                    Physics.Raycast(ray.origin + ray.direction * 100f, -ray.direction, out hit, Mathf.Infinity, layerMask_sphere);
                }
                else
                {
                    Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask_sphere);
                }
                if (hit.point != Vector3.zero)
                {
                    transform.position = hit.point;
                    myRenderer.SetPosition(1, transform.position);
                    AxisDataManager.Instance().changeVectorAt(index, transform.position);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hits = Physics.RaycastAll(ray, Mathf.Infinity, layerMask_axis);
                    for (int i = 0; i < hits.Length; i++)
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
    }

    void OnDrawGizmos()
    {
        //Debug.DrawRay(ray.origin + ray.direction * 100f, -ray.direction * 100f, Color.red);
    }
}
