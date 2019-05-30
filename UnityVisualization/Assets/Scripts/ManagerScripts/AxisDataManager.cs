using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisDataManager{ 
    private static AxisDataManager _instance;
    private List<Axis> axisArray = new List<Axis>();

    private int sampleSize = 10;

    public struct calculAxis
    {
        public Vector4 _color;
        public Vector3 position;
    }

    public static AxisDataManager Instance()
    {
        if(_instance == null)
        {
            _instance = new AxisDataManager();
        }
        return _instance;
    }

    public calculAxis[] GetAxisDatas()
    {
        calculAxis[] data = new calculAxis[axisArray.Count];

        for(int i = 0; i < axisArray.Count; i++)
        {
            data[i]._color = axisArray[i].color;
            data[i].position = axisArray[i].vector;
        }

        return data;
    }

    public calculAxis[] GetAxisSamples()
    {
        calculAxis[] data = new calculAxis[sampleSize];

        data[0]._color = Color.blue;
        data[0].position = new Vector3(1,0,0) * 20;
        data[1]._color = Color.red;
        data[1].position = new Vector3(0,1,0) * 20;
        data[2]._color = Color.green;
        data[2].position = new Vector3(0,0,1) * 20;

        /*
        data[0]._color = Color.blue;
        data[0].position = new Vector3(Random.Range(-20,20), Random.Range(-20, 20), Random.Range(-20, 20)).normalized * 20;
        data[1]._color = Color.red;
        data[1].position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)).normalized * 20;
        data[2]._color = Color.green;
        data[2].position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)).normalized * 20;
        data[3]._color = Color.cyan;
        data[3].position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)).normalized * 20;
        data[4]._color = Color.magenta;
        data[4].position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)).normalized * 20;
        data[5]._color = Color.yellow;
        data[5].position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)).normalized * 20;
        data[6]._color = Color.white;
        data[6].position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)).normalized * 20;*/
        return data;   
    }

    public Weight[] GetSampleWeights()
    {
        Weight[] data = new Weight[sampleSize * 10];

        for (int i = 0; i < sampleSize-1; i++)
        {
            for (int index = 0; index < Random.Range(3,10); index++)
            {
                data[i * 10 + index].propertyIndex = Random.Range(1, 4);
                data[i * 10 + index].weight = Random.Range(0.5f, 1.0f);
            }
        }

        return data;
    }

    public Weight[] GetWeights()
    {
        Weight[] data = new Weight[axisArray.Count * 10];

        for(int i = 0; i < axisArray.Count; i++)
        {
            for(int index = 0; index < axisArray[i].weights.Count; index++)
            {
                data[i * 10 + index].propertyIndex = axisArray[i].weights[index].propertyIndex;
                data[i * 10 + index].weight = axisArray[i].weights[index].weight;
            }
        }

        return data;
    }

    public int GetAxisCount()
    {
        return axisArray.Count;
    }

    public int GetAxisSampleCount()
    {
        return sampleSize;
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