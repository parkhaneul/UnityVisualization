using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSetting : MonoBehaviour
{
    //public GameObject vectorController;

	public PropertyListContent propertyListContent;

    public propertySetting propertyController;

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
		//propertyController.setProperty();
	}

	public void OnClickAddPropertySelector()
	{

		GameObject g = GameObject.Instantiate(propertyController.gameObject);
		g.transform.parent = propertyListContent.transform;
		g.transform.localScale = Vector3.one;
		Debug.Log("propertyselector추가 기능을 구현해야 합니다.");
		//propertyselector추가

	}
}
