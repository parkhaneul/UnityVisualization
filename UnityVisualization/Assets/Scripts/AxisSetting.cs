using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSetting : MonoBehaviour
{
    public GameObject vectorController;
    public propertySetting propertyController;

    private Axis currentAxis;
    private int index;

    public void setAxis(int _index)
    {
        index = _index;
        currentAxis = SetDropDown.myAxis[index];
        setProperty();
    }

    public void setVector()
    {
        var temp = currentAxis.vector;
    }

    public void setProperty()
    {
        //propertyController.setProperty();
    }
}
