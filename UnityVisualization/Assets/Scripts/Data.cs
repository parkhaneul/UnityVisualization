using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public struct Data
{
    public string[] strData;
    public float[] floatData;

    public Data(string str)
    {
        var temp = Regex.Split(str, FileReader.SPLIT_RE);
        float f;
        int floatIndex = 0;
        int strIndex = 0;
        string value;
        strData = new string[MetaData.strIndex];
        floatData = new float[MetaData.floatIndex];
        for (int i = 0; i < temp.Length; i++)
        {
            value = temp[i].TrimStart('\"').TrimEnd('\"').Replace("\\", "");
            if (float.TryParse(value, out f))
            {
                floatData[floatIndex] = f;
                floatIndex++;
            }
            else
            {
                strData[strIndex] = temp[i];
                strIndex++;
            }
        }
    }
}

public struct Axis
{
    public Vector3 vector;
    public List<Weight> weights;
}

public struct Weight{
    public int propertyIndex;
    public float weight;
}