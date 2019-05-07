using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSetting : MonoBehaviour
{
    //public GameObject vectorController;

	public PropertyListContent propertyListContent;

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    public propertySetting propertyController;
=======
	private List<propertySetting> propertySettings = new List<propertySetting>();
>>>>>>> parent of 1bc4796... delete modify
=======
	private List<propertySetting> propertySettings = new List<propertySetting>();
>>>>>>> parent of 1bc4796... delete modify
=======
	private List<propertySetting> propertySettings = new List<propertySetting>();
>>>>>>> parent of 1bc4796... delete modify
=======
	private List<propertySetting> propertySettings = new List<propertySetting>();
>>>>>>> parent of 1bc4796... delete modify

    private Axis currentAxis;
    private int index;
	

	public void setAxis(int _index)
    {
        index = _index;
        currentAxis = SetDropDown.myAxis[index];
		
        ChangePropertyInfo();
    }

    public void setVector()
    {
        var temp = currentAxis.vector;
    }

    public void ChangePropertyInfo()
    {
		foreach(Transform p in propertyListContent.transform)
		{
			GameObject.Destroy(p.gameObject);
		}
<<<<<<< HEAD
		//propertyController.setProperty();
=======
		propertySettings.Clear();
		foreach (Weight w in currentAxis.weights)
		{
			Debug.Log(w.propertyIndex + " :: " + w.weight);
			ActiveProperty(w);
		}
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 1bc4796... delete modify
=======
>>>>>>> parent of 1bc4796... delete modify
=======
>>>>>>> parent of 1bc4796... delete modify
=======
>>>>>>> parent of 1bc4796... delete modify
	}

	public void OnClickAddPropertySelector()
	{
		/*
		//SetDropDown.myAxis[index].weights를 저장하는 코드!;
		Weight w = new Weight();
		Axis temp = SetDropDown.myAxis[index];
		temp.weights.Add(w);


		GameObject g = GameObject.Instantiate(propertyController.gameObject);
		g.transform.parent = propertyListContent.transform;
		g.transform.localScale = Vector3.one;
<<<<<<< HEAD
		Debug.Log("propertyselector추가 기능을 구현해야 합니다.");
		//propertyselector추가
		*/

=======
		g.GetComponent<propertySetting>().dropdown.value = w.propertyIndex;
		g.GetComponent<propertySetting>().slider.value = w.weight;
		g.GetComponent<propertySetting>().text.text = w.weight.ToString();
		g.GetComponent<propertySetting>().SettingIndex = propertySettings.Count;
		propertySettings.Add(g.GetComponent<propertySetting>());
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 1bc4796... delete modify
=======
>>>>>>> parent of 1bc4796... delete modify
=======
>>>>>>> parent of 1bc4796... delete modify
=======
>>>>>>> parent of 1bc4796... delete modify
	}
}
