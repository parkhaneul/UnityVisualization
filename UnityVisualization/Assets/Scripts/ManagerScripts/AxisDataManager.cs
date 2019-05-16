﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisDataManager{ 
    private static AxisDataManager _instance;
    private List<Axis> axisArray = new List<Axis>();

    public struct AxisData
    {
        public Color _color;
        public Vector3 _vector;
    }

    public static AxisDataManager Instance()
    {
        if(_instance == null)
        {
            _instance = new AxisDataManager();
        }
        return _instance;
    }

    public AxisData[] GetAxisDatas()
    {
        AxisData[] data = new AxisData[axisArray.Count];

        for(int i = 0; i < axisArray.Count; i++)
        {
            data[i]._color = axisArray[i].color;
            data[i]._vector = axisArray[i].vector;
        }

        return data;
    }

    public Weight[] GetWeights()
    {
        Weight[] data = new Weight[axisArray.Count * 10];

        for(int i = 0; i < axisArray.Count-1; i++)
        {
            for(int index = 0; i < axisArray[i].weights.Count; i++)
            {
                data[i * 10 + index] = axisArray[i].weights[index];
            }
        }

        return data;
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

    public void AddAxis(Axis axis)
    {
        axisArray.Add(axis);
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

    public void changeVectorAt(int index, Vector3 vector)
    {
        var temp = GetAxis(index);
        temp.vector = vector;
        ChangeAxisAt(index, temp);
    }
}