using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MetaData{
	public static string[] metaData;
	public static int metaNum;
	
	public static void MetaDataInit(string meta)
	{
		string[] temp = meta.Split(',');
		metaData = temp;
		metaNum = temp.Length;
	}
}
