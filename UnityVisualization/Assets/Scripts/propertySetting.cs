using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class propertySetting : MonoBehaviour
{
    public Dropdown dropdown;
    public Slider slider;
    public Text text;
<<<<<<< HEAD
    public Weight currentWeight;
=======
	public int SettingIndex;

	//private int propertyIndex;
    //public Weight currentWeight;
>>>>>>> parent of 1bc4796... delete modify

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
<<<<<<< HEAD
        var temp = slider.value * 0.01f;
        text.text = temp.ToString();
    }

=======
        text.text = slider.value.ToString();

		Weight temp = SetDropDown.myAxis[AxisSetting.index].weights[SettingIndex];
		temp.propertyIndex = dropdown.value;
		temp.weight = slider.value;
		SetDropDown.myAxis[AxisSetting.index].weights[SettingIndex] = temp;
	}
	/*
>>>>>>> parent of 1bc4796... delete modify
    public Weight GetWeight()
    {
        var temp = new Weight();
        temp.propertyIndex = dropdown.value;
        temp.weight = slider.value;
        return temp;
    }
<<<<<<< HEAD
=======
	*/
>>>>>>> parent of 1bc4796... delete modify
}
