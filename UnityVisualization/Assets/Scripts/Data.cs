using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
	private Dictionary<string, string> data = new Dictionary<string, string>();

	public Data(string s)
	{
		string[] temp = s.Split(',');
		for(int i = 0; i < MetaData.metaNum; i++)
		{
			data.Add(MetaData.metaData[i], temp[i]);
		}
	}

	public Dictionary<string, string> GetData()
	{
		return data;
	}
}
