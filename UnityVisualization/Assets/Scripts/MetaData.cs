using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public struct MetaData
{
    public static int[] strArray;
    public static int[] floatArray;
    public static int floatIndex;
    public static int strIndex;

    public MetaData(string str)
    {
        floatIndex = 0;
        strIndex = 0;
        var temp = Regex.Split(str, FileReader.SPLIT_RE);
        float f;
        string value;
        for (int i = 0; i < temp.Length; i++)
        {
            value = temp[i].TrimStart('\"').TrimEnd('\"').Replace("\\", "");
            if (float.TryParse(value, out f))
            {
                floatIndex++;
            }
            else
            {
                strIndex++;
            }
        }
        strArray = new int[strIndex];
        floatArray = new int[floatIndex];
        strIndex = 0;
        floatIndex = 0;
        for (int i = 0; i < temp.Length; i++)
        {
            value = temp[i].TrimStart('\"').TrimEnd('\"').Replace("\\", "");
            if (float.TryParse(value, out f))
            {
                floatArray[floatIndex] = i;
                floatIndex++;
            }
            else
            {
                strArray[strIndex] = i;
                strIndex++;
            }
        }
    }
}

public struct Header
{
    public static List<string> data;

    public Header(string str)
    {
        data = new List<string>();
        var temp = Regex.Split(str, ",");
        foreach(string item in temp)
        {
            data.Add(item);
        }
    }

    public int headerCount() {
        return data.Count;
    }
}
