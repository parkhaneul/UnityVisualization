using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetDropDown : MonoBehaviour
{
	
    public AxisSetting axisSetting;
    public Dropdown dropdown;
	public InputField InputText;

    public GameObject axisModel;
    public GameObject axisSpace;
    private static List<GameObject> axisList = new List<GameObject>();

	public static int currentIndex = 0;

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
        var myAxis = AxisDataManager.Instance();

        InputText.text = InputText.text;
        Axis temp = myAxis.GetAxis(currentIndex);
		temp.name = InputText.text;
		myAxis.ChangeAxisAt(currentIndex,temp);
		dropdown.options[dropdown.value].text = InputText.text;
	}

    private void addValue()
    {
        var myAxis = AxisDataManager.Instance();

        var newAxis = new Axis();
		newAxis.weights = new List<Weight>();
		newAxis.name = "new Axis";
        var temp = Instantiate(axisModel, axisSpace.transform);
        temp.GetComponent<AxisRepositioner>().index = myAxis.GetAxisCount();
        temp.SetActive(true);
        axisList.Add(temp);
        myAxis.AddAxis(newAxis);
		dropdown.options.Add(new Dropdown.OptionData(newAxis.name));
	}

    private void deleteValue()
    {
        var myAxis = AxisDataManager.Instance();

        var value = dropdown.value;
        myAxis.RemoveAxisAt(value);
        var temp = axisList[value];
        for(int i = value; i <= myAxis.GetAxisCount(); i++)
        {
            axisList[i].GetComponent<AxisRepositioner>().index -= 1;
        }
        axisList.RemoveAt(value);
        Destroy(temp);
        dropdown.options.RemoveAt(value);
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    public void changeAxis(int index)
    {
        var myAxis = AxisDataManager.Instance();

        currentIndex = index;
		axisSetting.SetAxis(index);
		InputText.text = myAxis.GetAxis(index).name;
	}

    public static void changeColor(Color _color) {
        axisList[currentIndex].GetComponent<MeshRenderer>().material.color = _color;
    }
}
