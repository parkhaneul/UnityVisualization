using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRepositioner : MonoBehaviour
{
    public Camera view;
    public float value = 1;
    public LineRenderer myRenderer;
    private bool isClicked = false;

    Ray ray;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        RaycastHit[] hits;
        Vector3 mpNormalized = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 1);
        ray = view.ViewportPointToRay(mpNormalized);
        Debug.Log(ray.origin);
        int layerMask_sphere = 1 << 9;
        int layerMask_axis = 1 << 10;

        if (isClicked)
        {
            if(Vector3.Dot(view.transform.forward,transform.position) >= 0)
            {
                Physics.Raycast(ray.origin + view.transform.forward * 100f, (-view.transform.forward - ray.direction.normalized * - 3/11) * 1000f,out hit, Mathf.Infinity, layerMask_sphere);
            }else
            {
                Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask_sphere);
            }
            transform.position = hit.point;
            myRenderer.SetPosition(1, transform.position);
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

    void OnDrawGizmos()
    {
        Debug.DrawRay(ray.origin + view.transform.forward * 100f, -view.transform.forward * 100f, Color.red);
    }
}
