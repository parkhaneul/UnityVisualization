using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public GameObject Content;
	public GameObject PropertySelector;
    public void OnClickAddPropertySelector()
	{
		GameObject g = GameObject.Instantiate(PropertySelector);
		g.transform.parent = Content.transform;
		g.transform.localScale = Vector3.one;
		Debug.Log("propertyselector추가 기능을 구현해야 합니다.");
		//propertyselector추가
	}
}
