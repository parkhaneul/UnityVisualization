using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

public struct Data
{
    Color _color;
    Vector3 position;
}

public struct Cluster
{
    public Vector3 position;
    public Vector3 lastPosition;
    public Vector4 _color;
    public uint index;
}

public struct Particle
{
    public Vector4 _color;
    public Vector3 position;
}

/*public struct Data
{
    //public string[] strData;
    public float[] floatData;

    public Data(string str)
    {
        var temp = Regex.Split(str, FileReader.SPLIT_RE);
        float f;
        int floatIndex = 0;
        //int strIndex = 0;
        string value;
        //strData = new string[MetaData.strIndex];
        floatData = new float[MetaData.floatIndex];
        for (int i = 0; i < temp.Length; i++)
        {
            value = temp[i].TrimStart('\"').TrimEnd('\"').Replace("\\", "");
            if (float.TryParse(value, out f))
            {
                floatData[floatIndex] = f;
                floatIndex++;
            }/*
            else
            {
                strData[strIndex] = temp[i];
                strIndex++;
            }
        }
    }
}*/

public struct Axis
{
    public Vector3 vector;
    public Color color;
    public List<Weight> weights;
	public string name;
}

public struct Weight{
    public int propertyIndex;
    public float weight;
}