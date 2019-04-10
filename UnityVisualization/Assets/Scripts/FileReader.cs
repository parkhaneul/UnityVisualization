using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class FileReader : MonoBehaviour {
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

	public List<Data> data = new List<Data>();



	private string filePath = "Assets/Resources/";
    //경로에 파일 넣으시고 파일 이름 .csv dataName에 명시해주세요.
    private string dataName = "data.csv";

    void Start()
    {
		try
		{
			StreamReader file = new StreamReader(filePath + dataName);

			var lines = Regex.Split(file.ReadToEnd(), LINE_SPLIT_RE);

			MetaData.MetaDataInit(lines[0]);
			for(int i = 1; i < lines.Length; i++)
			{
				if(lines[i] !=null)
					data.Add(new Data(lines[i]));
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
