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
        dropdown.options.RemoveAll(null);
        for(int i = 0; i < MetaData.floatArray.Length; i++)
        {
            var temp = new Dropdown.OptionData(Header.data[MetaData.floatArray[i]]);
            dropdown.options.Add(temp);
        }
    }

    public void changeValue()
    {
        text.text = slider.value.ToString();
        var myAxis = AxisDataManager.Instance();
        var temp = myAxis.GetAxis(AxisSetting.index);
        var weight = myAxis.GetAxis(AxisSetting.index).weights[SettingIndex];
		weight.propertyIndex = dropdown.value;
		weight.weight = slider.value;
        temp.weights[SettingIndex] = weight;
        myAxis.ChangeAxisAt(AxisSetting.index, temp);
	}


	public void OnClickMinusButton()
	{
        var myAxis = AxisDataManager.Instance();
        var temp = myAxis.GetAxis(AxisSetting.index);
        temp.weights.RemoveAt(SettingIndex);
        myAxis.ChangeAxisAt(AxisSetting.index, temp);
        AxisSetting.DeleteProperty(SettingIndex);
	}
}
