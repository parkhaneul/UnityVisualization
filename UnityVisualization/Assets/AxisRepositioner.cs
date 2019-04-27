using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRepositioner : MonoBehaviour
{
    public Camera view;
    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = view.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButton(0))
        {
            plane.transform.LookAt(view.transform);
            this.transform.position = new Vector3(hit.point.x,hit.point.y,0);
        }
    }
}
