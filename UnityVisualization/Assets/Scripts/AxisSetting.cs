using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSetting : MonoBehaviour
{
	//public GameObject vectorController;

	public PropertyListContent propertyListContent;
    public GameObject propertyController;

	private static List<propertySetting> propertySettings = new List<propertySetting>();

    private Axis currentAxis;
	public static int index;
	

	public void SetAxis(int _index)
    {
        index = _index;
        currentAxis = AxisDataManager.Instance().GetAxis(index);

        ChangePropertiesInfo();
    }

    private void ChangePropertiesInfo()
    {
		foreach(Transform p in propertyListContent.transform)
		{
			Destroy(p.gameObject);
		}
		propertySettings.Clear();
		for (int i = 0; i < currentAxis.weights.Count; i++)
		{
			ActiveProperty(currentAxis.weights[i]);
		}
		//Struct 배열에서 foreach 사용말자...ㅠㅠ
		/*
		foreach (Weight w in currentAxis.weights)
		{
			Debug.Log(w.propertyIndex + " :: " + w.weight);
			ActiveProperty(w);
		}
		*/
	}

	public void OnClickAddPropertySelector()
	{
		Weight w = new Weight();
		w.propertyIndex = 0;
		w.weight = 0;

        var myAxis = AxisDataManager.Instance();
        var temp = myAxis.GetAxis(index);
        temp.weights.Add(w);
        myAxis.ChangeAxisAt(index, temp);

		ActiveProperty(w);
	}

	private void ActiveProperty(Weight w)
	{
		GameObject g = Instantiate(propertyController);
		g.transform.parent = propertyListContent.transform;
		g.transform.localScale = Vector3.one;
		g.GetComponent<propertySetting>().dropdown.value = w.propertyIndex;
		g.GetComponent<propertySetting>().slider.value = w.weight;
		g.GetComponent<propertySetting>().text.text = w.weight.ToString();
		g.GetComponent<propertySetting>().SettingIndex = propertySettings.Count;
		propertySettings.Add(g.GetComponent<propertySetting>());
	}

	public static void DeleteProperty(int index)
	{
		Destroy(propertySettings[index].gameObject);
		propertySettings.RemoveAt(index);
		for (int i = 0; i < propertySettings.Count; i++)
		{
			propertySettings[i].SettingIndex = i;
			//Debug.Log(i + "::" + SetDropDown.myAxis[AxisSetting.index].weights[i].propertyIndex + "::" + SetDropDown.myAxis[AxisSetting.index].weights[i].weight);
		}
	}
}
