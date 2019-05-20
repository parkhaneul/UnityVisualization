using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSetting : MonoBehaviour
{
	//public GameObject vectorController;

	public PropertyListContent propertyListContent;
    public GameObject propertyController;

    //private static List<propertySetting> propertySettings = new List<propertySetting>();
    public GameObject parent;
    private static List<GameObject> parents = new List<GameObject>(1);

    private Axis currentAxis;
	public static int index;
	

	public void SetAxis(int _index)
    {
        index = _index;
        currentAxis = AxisDataManager.Instance().GetAxis(index);

        ChangePropertiesInfo();
    }

    public void addAxis()
    {
        var o = Instantiate(parent);
        parents.Add(o);
    }

    public void removeAxis()
    {
        parents.RemoveAt(index);
    }

    private void ChangePropertiesInfo()
    {
        foreach(GameObject o in parents)
        {
            o.SetActive(false);
        }
        parents[index].SetActive(true);
        /*
		foreach(Transform p in propertyListContent.transform)
		{
			Destroy(p.gameObject);
		}
		propertySettings.Clear();
		for (int i = 0; i < currentAxis.weights.Count; i++)
		{
			ActiveProperty(currentAxis.weights[i]);
		}*/
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
		g.transform.SetParent(parents[index].transform);
		//g.transform.localScale = Vector3.one;
		g.GetComponent<propertySetting>().setWeight(w);
        g.GetComponent<propertySetting>().SettingIndex = index;
	}

	public static void DeleteProperty(int _index)
	{
		Destroy(parents[index].transform.GetChild(_index));
		for (int i = 0; i < parents[index].transform.childCount; i++)
		{
			parents[index].transform.GetChild(_index).GetComponent<propertySetting>().SettingIndex = i;
		}
	}
}
