using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class propertySetting : MonoBehaviour
{
    public Dropdown dropdown;
    public Slider slider;
    public Text text;
	public int SettingIndex;

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
	/*
    public Weight GetWeight()
    {
        var temp = new Weight();
        temp.propertyIndex = dropdown.value;
        temp.weight = slider.value;
        return temp;
    }
	*/
}
