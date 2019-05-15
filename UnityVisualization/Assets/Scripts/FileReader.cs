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

	private string filePath = "Assets/Resources/";
    //경로에 파일 넣으시고 파일 이름 .csv dataName에 명시해주세요.
    private string dataName = "data.csv";

    void Awake()
    {
		try
		{
			StreamReader file = new StreamReader(filePath + dataName);

			var lines = Regex.Split(file.ReadToEnd(), LINE_SPLIT_RE);

            header = new Header(lines[0]);
            dataList = new Data[lines.Length - 1];
            meta = new MetaData(lines[1]);
            meta = new MetaData(lines[1]);
            for (int i = 1; i < lines.Length; i++)
			{
                dataList[i-1] = new Data(lines[i]);
			}
			file.Close();
		}
		catch (IOException e)
		{
			Debug.Log("The file could not be read:");
			Debug.Log(e.Message);
		}
	}
}
