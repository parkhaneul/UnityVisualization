using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorSelect : MonoBehaviour
{
    public Axis axis;
    public AxisVisualization av;
    public Text text;

    public Slider xSlider;
    public Slider ySlider;
    public Slider zSlider;

    public void setVectorX(float x)
    {
        var v = axis.vector;
        axis.vector = new Vector3(x, v.y, v.z);
        changeText();
    }

    public void setVectorY(float y)
    {
        var v = axis.vector;
        axis.vector = new Vector3(v.x, y, v.z);
        changeText();
    }

    public void setVectorZ(float z)
    {
        var v = axis.vector;
        axis.vector = new Vector3(v.y, v.y, z);
        changeText();
    }

    public void changeText()
    {
        av.axisVisualize(axis);
        var vector = av.model.transform.rotation.eulerAngles;
        text.text = "Vector (" + vector.x + " , " + vector.y + " , " + vector.z + ")";
        xSlider.value = vector.x;
        ySlider.value = vector.y;
        zSlider.value = vector.z;
    }
}
