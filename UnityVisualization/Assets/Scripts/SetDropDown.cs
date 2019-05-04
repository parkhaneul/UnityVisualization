using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetDropDown : MonoBehaviour
{
    public static List<Axis> myAxis = new List<Axis>();
	
    public AxisSetting axisSetting;
    public Dropdown dropdown;
	public InputField InputText;

	private int currentIndex = 0;

	private void Start()
	{
		addValue();
	}

	public void OnClickPlusButton()
	{
		addValue();
	}

	public void OnClickMinusButton()
	{
		deleteValue();
	}

	public void OnNameChange()
	{
		InputText.text = InputText.text;
		Axis temp = myAxis[currentIndex];
		temp.name = InputText.text;
		myAxis[currentIndex] = temp;
		dropdown.options[dropdown.value].text = InputText.text;
	}
    

    private void addValue()
    {
        var newAxis = new Axis();
		newAxis.weights = new List<Weight>();
		newAxis.name = "new Axis";
		myAxis.Add(newAxis);
		dropdown.options.Add(new Dropdown.OptionData(newAxis.name));
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
		currentIndex = index;
		axisSetting.SetAxis(index);
		InputText.text = myAxis[index].name;
	}
}
