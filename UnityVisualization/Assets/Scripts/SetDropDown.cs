using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetDropDown : MonoBehaviour
{
    public static List<Axis> myAxis = new List<Axis>();
	
    public AxisSetting axisSetting;
    public Dropdown dropdown;

	private int currentIndex = 0;

	public void OnClickPlusButton()
	{
		addValue("new Axis");
	}

	public void OnClickMinusButton()
	{
		deleteValue();
	}
    

    private void addValue(string str)
    {
        var newAxis = new Axis();
        myAxis.Add(newAxis);
        dropdown.options.Add(new Dropdown.OptionData(str));
    }

    private void deleteValue()
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
