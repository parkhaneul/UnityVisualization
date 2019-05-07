using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyListContent : MonoBehaviour
{
    public GameObject PropertySelector;
	public Weight weights;

	public void OnClickAddPropertySelector()
	{

		GameObject g = GameObject.Instantiate(PropertySelector);
		g.transform.parent = this.transform;
		g.transform.localScale = Vector3.one;
		Debug.Log("propertyselector추가 기능을 구현해야 합니다.");
	}
}
