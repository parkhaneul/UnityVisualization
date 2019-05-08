using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisViewModel{
    private int axisIndex = 0;

    public AxisViewModel(int index)
    {
        axisIndex = index;
    }

    public void setIndex(int index)
    {
        axisIndex = index;
    }

    public void setAxis(Axis _axis)
    {
        DataManager.Instance().changeAxisAt(axisIndex, _axis);
    }

    public Axis getAxis(int _index)
    {
        return DataManager.Instance().getAxis(_index);
    }

    public Axis getAxis()
    {
        return getAxis(axisIndex);
    }

    public Vector3 getVector3()
    {
        return getAxis().vector;
    }

    public Color getColor()
    {
        return getAxis().color;
    }

    public void setVector3(Vector3 _vector) {
        var axis = getAxis();
        axis.vector = _vector;
        setAxis(axis);
    }

    public void setColor(Color _color)
    {
        var axis = getAxis();
        axis.color = _color;
        setAxis(axis);
    }

    public void setName(string _name)
    {
        var axis = getAxis();
        axis.name = _name;
        setAxis(axis);
    }

    public void setWeight(List<Weight> _weights)
    {
        var axis = getAxis();
        axis.weights = _weights;
        setAxis(axis);
    }

    public int getWeightCount()
    {
        return getAxis().weights.Count;
    }

    public Weight getWeight(int index)
    {
        var temp = Mathf.Clamp(index, 0, getAxis().weights.Count-1);
        return getAxis().weights[temp];
    }
}
