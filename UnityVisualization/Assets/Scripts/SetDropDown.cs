using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetDropDown : MonoBehaviour
{
    public static List<Axis> myAxis = new List<Axis>();
    public AxisSetting axisSetting;
    public propertySetting propertySetting;
    public Dropdown dropdown;

    private int currentIndex = 0;

    public void addValue(string str = "new Axis")
    {
        var newAxis = new Axis();
        myAxis.Add(newAxis);
        dropdown.options.Add(new Dropdown.OptionData(str));
    }

    public void deleteValue()
    {
        var value = dropdown.value;
        myAxis.RemoveAt(value);
        dropdown.options.RemoveAt(value);
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    public void changeAxis(int index)
    {
        axisSetting.setAxis(index);
    }
}
