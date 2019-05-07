using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class propertySetting : MonoBehaviour
{
    public Dropdown dropdown;
    public Slider slider;
    public Text text;
	public int SettingIndex; // Axis.weights에서의 setting의 인덱스

	//private int propertyIndex;
    //public Weight currentWeight;

    public void Start()
    {
        for(int i = 0; i < MetaData.floatArray.Length; i++)
        {
            var temp = new Dropdown.OptionData(Header.data[MetaData.floatArray[i]]);
            dropdown.options.Add(temp);
        }
    }

    public void changeValue()
    {
        text.text = slider.value.ToString();
		Weight temp = SetDropDown.myAxis[AxisSetting.index].weights[SettingIndex];
		temp.propertyIndex = dropdown.value;
		temp.weight = slider.value;
		SetDropDown.myAxis[AxisSetting.index].weights[SettingIndex] = temp;
	}


	public void OnClickMinusButton()
	{
		SetDropDown.myAxis[AxisSetting.index].weights.RemoveAt(SettingIndex);
		AxisSetting.DeleteProperty(SettingIndex);
	}
}
