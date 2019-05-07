using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSetting : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject vectorController;
    public propertySetting propertyController;
    public DataManagement manager;

    private int index;
=======
	//public GameObject vectorController;

	public PropertyListContent propertyListContent;
    public GameObject propertyController;

	private List<propertySetting> propertySettings = new List<propertySetting>();

    private Axis currentAxis;
	public static int index;
	
>>>>>>> 1bc47965b7eb2c45804ed2329bde0353febe538d

	public void SetAxis(int _index)
    {
        index = _index;
<<<<<<< HEAD
    }

    public void setVector(Vector3 _vector)
    {
        manager.axisArray[index].setVector(_vector);
    }

    public void setColor(Color _color)
    {
        manager.axisArray[index].setColor(_color);
    }
=======
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

	public static void DeleteProperty(int index)
	{
		GameObject.Destroy(propertySettings[index].gameObject);
		propertySettings.RemoveAt(index);
		for (int i = 0; i < propertySettings.Count; i++)
		{
			propertySettings[i].SettingIndex = i;
			Debug.Log(i + "::" + SetDropDown.myAxis[AxisSetting.index].weights[i].propertyIndex + "::" + SetDropDown.myAxis[AxisSetting.index].weights[i].weight);
		}
	}
>>>>>>> 1bc47965b7eb2c45804ed2329bde0353febe538d
=======
>>>>>>> parent of 1bc4796... delete modify
=======
>>>>>>> parent of 1bc4796... delete modify
=======
>>>>>>> parent of 1bc4796... delete modify
}
