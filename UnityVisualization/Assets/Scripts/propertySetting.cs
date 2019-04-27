using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class propertySetting : MonoBehaviour
{
    public Dropdown dropdown;
    public Slider slider;
    public Text text;

    public Weight currentWeight;

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
        var temp = slider.value * 0.01f;
        text.text = temp.ToString();
    }

    public Weight GetWeight()
    {
        var temp = new Weight();
        temp.propertyIndex = dropdown.value;
        temp.weight = slider.value;
        return temp;
    }
}
