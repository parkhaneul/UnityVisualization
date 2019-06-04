using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class FileReader : MonoBehaviour {
    public static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    public static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    public static char[] TRIM_CHARS = { '\"' };

    static Header header;
    static MetaData meta;
    public static Data[] dataList;
    public static float[] floatList;

	private string filePath = "Assets/Resources/";
    //경로에 파일 넣으시고 파일 이름 .csv dataName에 명시해주세요.
    private string dataName = "data.csv";

    void Awake()
    {
		try
		{
			StreamReader file = new StreamReader(filePath + dataName);

			var lines = Regex.Split(file.ReadToEnd(), LINE_SPLIT_RE);
            float f;

            header = new Header(lines[0]);
            meta = new MetaData(lines[1]);

            dataList = new Data[lines.Length - 1];
            floatList = new float[lines.Length * 50];

            for (int i = 1; i < lines.Length; i++)
            {
                var temp = Regex.Split(lines[i], ",");
                var stringCount = 0;
                for(int index = 0; index < temp.Length; index++)
                {
                    string value = temp[index].TrimStart('\"').TrimEnd('\"').Replace("\\", "");
                    if (float.TryParse(value,out f))
                    {
                        floatList[(i - 1) * 50 + index - stringCount] = f;
                    }
                    else
                    {
                        stringCount++;
                    }
                }
			    }

            file.Close();
            dataNormalize();
		}
		catch (IOException e)
		{
			Debug.Log("The file could not be read:");
			Debug.Log(e.Message);
		}
	}

    void dataNormalize()
    {
        for(int i = 0; i < 50; i++) {
            var min = float.MaxValue;
            var max = float.MinValue;
            for (int index = 0; index < dataList.Length; index++) {
                var temp = floatList[index * 50 + i];
                if(temp > max)
                {
                    max = temp;
                }
                if(temp < min)
                {
                    min = temp;
                }
            }

            for (int index = 0; index < dataList.Length; index++)
            {
                floatList[index * 50 + i] = (floatList[index * 50 + i] - min) / (max - min);
            }
        }
    }
}
