using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisDataManager{ 
    private static AxisDataManager _instance;
    private List<Axis> axisArray = new List<Axis>();

	public static AxisDataManager Instance()
    {
        if(_instance == null)
        {
            _instance = new AxisDataManager();
        }
        return _instance;
    }

	public int GetAxisCount()
    {
        return axisArray.Count;
    }

	//초기 Axis 설정
	public void AddAxis() {
		Axis newAxis = new Axis();
		newAxis.vector = new Vector3(0, 0, 0);
		newAxis.color = new Color(0, 0, 0);
		newAxis.weights = new List<Weight>();
		newAxis.name = "new Axis";
		axisArray.Add(newAxis);
	}

    public Axis GetAxis(int index)
    {
        return axisArray[index];
    }

    public List<Axis> GetAxisAll()
    {
        return axisArray;
    }

    public void RemoveAxis(Axis axis)
    {
        axisArray.Remove(axis);
    }

    public void RemoveAxisAt(int index)
    {
        axisArray.RemoveAt(index);
    }

    public void ChangeAxisAt(int index, Axis axis)
	{
        axisArray[index] = axis;
    }
}