using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSetting : MonoBehaviour
{
    public GameObject vectorController;
    public propertySetting propertyController;
    public DataManagement manager;

    private int index;

    public void setAxis(int _index)
    {
        index = _index;
    }

    public void setVector(Vector3 _vector)
    {
        manager.axisArray[index].setVector(_vector);
    }

    public void setColor(Color _color)
    {
        manager.axisArray[index].setColor(_color);
    }
}
