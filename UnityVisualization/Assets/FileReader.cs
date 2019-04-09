using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class FileReader : MonoBehaviour {
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public List<MetaData> list = new List<MetaData>();

    private string filePath = "Assets/Resources/";
    //경로에 파일 넣으시고 파일 이름 .csv dataName에 명시해주세요.
    private string dataName = "";

    void Start()
    {
        StreamReader file = new StreamReader(filePath + dataName);
        var lines = Regex.Split(file.ReadToEnd(), LINE_SPLIT_RE);
        foreach(string line in lines)
        {
            //Metadata 생성해주세요.
        }
        file.Close();
    }
}
