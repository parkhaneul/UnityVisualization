using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetCurrentAxis : MonoBehaviour
{
<<<<<<< HEAD:UnityVisualization/Assets/Scripts/SetCurrentAxis.cs
    public DataManagement manager;
    public Dropdown dropdown;
    private int currentIndex = 0;
=======
    public static List<Axis> myAxis = new List<Axis>();
	
    public AxisSetting axisSetting;
    public Dropdown dropdown;
	public InputField InputText;

	private int currentIndex = 0;
>>>>>>> 1bc47965b7eb2c45804ed2329bde0353febe538d:UnityVisualization/Assets/Scripts/SetDropDown.cs

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
		Debug.Log(currentIndex + " " + myAxis[currentIndex].name + "::" + InputText.text);
		dropdown.options[dropdown.value].text = InputText.text;
	}
    

    private void addValue()
    {
        var newAxis = new Axis();
<<<<<<< HEAD:UnityVisualization/Assets/Scripts/SetCurrentAxis.cs
<<<<<<< HEAD:UnityVisualization/Assets/Scripts/SetCurrentAxis.cs
        manager.axisArray.Add(newAxis);
        dropdown.options.Add(new Dropdown.OptionData(str));
    }
=======
		newAxis.weights = new List<Weight>();
		newAxis.name = "new Axis";
		myAxis.Add(newAxis);
		dropdown.options.Add(new Dropdown.OptionData(newAxis.name));
	}
>>>>>>> 1bc47965b7eb2c45804ed2329bde0353febe538d:UnityVisualization/Assets/Scripts/SetDropDown.cs
=======
		newAxis.name = "new Axis";
		myAxis.Add(newAxis);
		dropdown.options.Add(new Dropdown.OptionData(newAxis.name));
    }
>>>>>>> parent of 0da9fa0... 수정작업:UnityVisualization/Assets/Scripts/SetDropDown.cs

    private void deleteValue()
    {
        var value = dropdown.value;
        manager.axisArray.RemoveAt(value);
        dropdown.options.RemoveAt(value);
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    public void changeAxis(int index)
    {
<<<<<<< HEAD:UnityVisualization/Assets/Scripts/SetCurrentAxis.cs
        currentIndex = index;
    }

    public int getListCount()
    {
        return manager.axisArray.Count;
    }
=======
		currentIndex = index;
		axisSetting.setAxis(index);
		InputText.text = myAxis[index].name;
	}
>>>>>>> 1bc47965b7eb2c45804ed2329bde0353febe538d:UnityVisualization/Assets/Scripts/SetDropDown.cs
}
