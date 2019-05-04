using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager{
    private static DataManager _instance;
    private List<Axis> axisArray;

    protected DataManager()
    {
        axisArray = new List<Axis>();
    }

    public static DataManager Instance()
    {
        if(_instance == null)
        {
            _instance = new DataManager();
        }
        return _instance;
    }

    public int getAxisCount()
    {
        return axisArray.Count;
    }

    public void addAxis(Axis axis)
    {
        axisArray.Add(axis);
    }

    public Axis getAxis(int index)
    {
        var temp = Mathf.Clamp(index, 0, axisArray.Count - 1);
        return axisArray[temp];
    }

    public List<Axis> getAxisAll()
    {
        return axisArray;
    }

    public void removeAxis(Axis axis)
    {
        axisArray.Remove(axis);
    }

    public void removeAxisAt(int index)
    {
        var temp = Mathf.Clamp(index, 0, axisArray.Count - 1);
        axisArray.RemoveAt(temp);
    }

    public void changeAxisAt(int index, Axis axis)
    {
        var temp = Mathf.Clamp(index, 0, axisArray.Count - 1);
        axisArray[temp] = axis;
    }
}