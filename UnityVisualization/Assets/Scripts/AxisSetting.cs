using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSetting : MonoBehaviour
{
	//public GameObject vectorController;

	public PropertyListContent propertyListContent;
    public GameObject propertyController;

	private List<propertySetting> propertySettings = new List<propertySetting>();

    private Axis currentAxis;
	public static int index;
	

	public void SetAxis(int _index)
    {
        index = _index;
        currentAxis = SetDropDown.myAxis[index];
		
        ChangePropertiesInfo();
    }

    private void ChangePropertiesInfo()
    {
		foreach(Transform p in propertyListContent.transform)
		{
			GameObject.Destroy(p.gameObject);
		}
		propertySettings.Clear();
		foreach (Weight w in currentAxis.weights)
		{
			Debug.Log(w.propertyIndex + " :: " + w.weight);
			ActiveProperty(w);
		}
	}

	public void OnClickAddPropertySelector()
	{
		Weight w = new Weight();
		w.propertyIndex = 0;
		w.weight = 0;
		SetDropDown.myAxis[index].weights.Add(w);

		ActiveProperty(w);
	}

	private void ActiveProperty(Weight w)
	{
		GameObject g = GameObject.Instantiate(propertyController);
		g.transform.parent = propertyListContent.transform;
		g.transform.localScale = Vector3.one;
		g.GetComponent<propertySetting>().dropdown.value = w.propertyIndex;
		g.GetComponent<propertySetting>().slider.value = w.weight;
		g.GetComponent<propertySetting>().text.text = w.weight.ToString();
		g.GetComponent<propertySetting>().SettingIndex = propertySettings.Count;
		propertySettings.Add(g.GetComponent<propertySetting>());
	}
}
