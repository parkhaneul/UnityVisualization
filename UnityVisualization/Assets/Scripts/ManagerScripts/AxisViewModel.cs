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

    public Axis getAxis()
    {
        return DataManager.Instance().getAxis(axisIndex);
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
