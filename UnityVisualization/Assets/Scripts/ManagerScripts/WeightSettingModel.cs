using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightSettingModel : MonoBehaviour
{
	public Dropdown dropdown;
	public Slider slider;
	public Text value;
	public AxisViewManager axisViewModel;

	private int weightIndex;

	// Start is called before the first frame update
	public void Start()
	{
		for (int i = 0; i < MetaData.floatArray.Length; i++)
		{
			var temp = new Dropdown.OptionData(Header.data[MetaData.floatArray[i]]);
			dropdown.options.Add(temp);
		}
	}

	public void SetWeightIndex(int _index)
	{
		weightIndex = _index;
	}

	public int GetWeightIndex()
	{
		return weightIndex;
	}

	public void OnChangeValue()
	{
		Weight temp = axisViewModel.GetWeight(weightIndex);
		temp.weight = slider.value;
		value.text = slider.value.ToString();
		axisViewModel.SetWeight(weightIndex, temp);
	}

	public void OnChangePropertyIndex()
	{
        Weight temp = axisViewModel.GetWeight(weightIndex);
		temp.propertyIndex = dropdown.value;
		axisViewModel.SetWeight(weightIndex, temp);
		
	}

	public void OnClickDeleteButton()
	{
		axisViewModel.RemoveWeightAt(weightIndex);
	}
}
