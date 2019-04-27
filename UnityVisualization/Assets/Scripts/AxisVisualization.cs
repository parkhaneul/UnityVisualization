using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisVisualization : MonoBehaviour
{
    public GameObject model;
    public List<Axis> axis = new List<Axis>();
    public int size;

    public void axisVisualize(Axis _axis)
    {
        var line = model.GetComponent<LineRenderer>();
        line.SetPosition(1, new Vector3(0, 0, size));
        model.transform.rotation = Quaternion.Euler(_axis.vector);
    }
}
