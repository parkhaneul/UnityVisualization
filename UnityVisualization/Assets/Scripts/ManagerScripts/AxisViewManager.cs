using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//property의 상위 오브젝트여야함(listcontent)
public class AxisViewManager : MonoBehaviour {
	
	public Dropdown dropdown;
	public InputField InputText;
	public GameObject propertyController;
	public GameObject axisModel;
	public GameObject axisSpace;
	public GameObject NameLabel;

	private static AxisViewManager _instance;
	private int axisIndex;
	private List<List<WeightSettingModel>> propertyControllers = new List<List<WeightSettingModel>>();
	private List<GameObject> axisList = new List<GameObject>();

	private void Awake()
	{
		_instance = this;
	}

	public static AxisViewManager Instance()
	{
		return _instance;
	}

	private void Start()
	{
		AddNewAxis();
        AddNewAxis();
        AddNewAxis();
        axisIndex = 0;
	}

	public void OnClickPlusButton()
	{
		AddNewAxis();
	}

	public void OnClickMinusButton()
	{
		DeleteAxis();
	}

	public void AddNewAxis()
	{
		Axis newAxis = new Axis();
		newAxis.vector = new Vector3(0, 0, 0);
		newAxis.color = new Color(0, 0, 0);
        newAxis.weights = new List<Weight>();
        newAxis.name = "new Axis";
		AxisDataManager.Instance().AddAxis(newAxis);
		GameObject temp = Instantiate(axisModel, axisSpace.transform);
		GameObject label = Instantiate(NameLabel);
		label.SetActive(true);
		label.transform.parent = NameLabel.transform.parent;
		temp.GetComponent<AxisRepositioner>().nameLabel = label.GetComponent<Text>();
		temp.GetComponent<AxisRepositioner>().index = AxisDataManager.Instance().GetAxisCount()-1;
		temp.SetActive(true);
		axisList.Add(temp);
		dropdown.options.Add(new Dropdown.OptionData("new Axis"));
		propertyControllers.Add(new List<WeightSettingModel>());
	}

	public void DeleteAxis()
	{
		if (axisIndex > 0)
		{
			AxisDataManager.Instance().RemoveAxisAt(axisIndex);
			for(int i = 0; i < propertyControllers[axisIndex].Count; i++)
			{
				Destroy(propertyControllers[axisIndex][i]);
			}
			propertyControllers.RemoveAt(axisIndex);

			int value = dropdown.value;
			GameObject temp = axisList[value];
			for(int i = value; i <= AxisDataManager.Instance().GetAxisCount(); i++)
			{
				axisList[i].GetComponent<AxisRepositioner>().index -= 1;
			}
			axisList.RemoveAt(value);
			GameObject.Destroy(temp);

			dropdown.options.RemoveAt(axisIndex);
			dropdown.value = 0;
			dropdown.RefreshShownValue();
			OnChangeAxisIndex();
		}
		else
		{
			Debug.Log("Can't Delete Axis");
		}
	}

	public void OnNameChange()
	{
		InputText.text = InputText.text;
		Axis temp = AxisDataManager.Instance().GetAxis(axisIndex);
		temp.name = InputText.text;
		SetAxis(temp);
		axisList[axisIndex].GetComponent<AxisRepositioner>().ChangeName(temp.name);
		dropdown.options[dropdown.value].text = InputText.text;
	}

	public void OnClickAddWeightButton()
	{
		AddWeight();
		AddWeightSettingObject(GetWeight(GetWeightCount() - 1));
	}

	public void OnChangeAxisIndex()
    {
		foreach (WeightSettingModel p in propertyControllers[axisIndex])
		{
			p.gameObject.SetActive(false);
		}

		axisIndex = dropdown.value;
		InputText.text = GetAxis().name;

		foreach(WeightSettingModel p in propertyControllers[axisIndex])
		{
			p.gameObject.SetActive(true);
		}
    }

	private void AddWeightSettingObject(Weight w)
	{
		GameObject g = Instantiate(propertyController);
		g.transform.SetParent(this.transform);
        g.transform.localScale = Vector3.one;
		g.SetActive(true);
		g.GetComponent<WeightSettingModel>().SetWeightIndex(propertyControllers[axisIndex].Count);
        Debug.Log(propertyControllers[axisIndex].Count);
		g.GetComponent<WeightSettingModel>().axisViewModel = this;
		propertyControllers[axisIndex].Add(g.GetComponent<WeightSettingModel>());
		g.GetComponent<WeightSettingModel>().dropdown.value = w.propertyIndex;
		g.GetComponent<WeightSettingModel>().slider.value = w.weight;
		g.GetComponent<WeightSettingModel>().value.text = w.weight.ToString();
        //Debug.Log(axisIndex);
    }

    public void SetAxis(Axis _axis)
    {
		AxisDataManager.Instance().ChangeAxisAt(axisIndex, _axis);
    }

    public Axis GetAxis(int _index)
    {
        return AxisDataManager.Instance().GetAxis(_index);
    }

    public Axis GetAxis()
    {
        return GetAxis(axisIndex);
    }

    public Vector3 GetVector3()
    {
        return GetAxis().vector;
    }

    public Color GetColor()
    {
        return GetAxis().color;
    }

    public void SetVector3(Vector3 _vector) {
        var axis = GetAxis();
        axis.vector = _vector;
        SetAxis(axis);
    }

    public void SetColor(Color _color)
    {

        var axis = GetAxis();
        axis.color = _color;
        SetAxis(axis);
    }

    public void SetName(string _name)
    {
        var axis = GetAxis();
        axis.name = _name;
        SetAxis(axis);
    }

    public void SetWeight(int index, Weight w)
    {
		//Debug.Log("SetWeight" + index);
		Axis a = GetAxis();
		a.weights[index] = w;
		SetAxis(a);
    }

	//Weight 초기 값 설정
    public void AddWeight()
    {
        Weight w = new Weight();
        w.propertyIndex = 0;
        w.weight = 0;

        var axis = GetAxis();
        axis.weights.Add(w);
        SetAxis(axis);
    }

    public void RemoveWeightAt(int _index)
    {
        var axis = GetAxis();
        axis.weights.RemoveAt(_index);
		SetAxis(axis);

		GameObject.Destroy(propertyControllers[axisIndex][_index].gameObject);
		propertyControllers[axisIndex].RemoveAt(_index);

		for(int i = 0; i < propertyControllers[axisIndex].Count; i++) {
			propertyControllers[axisIndex][i].GetComponent<WeightSettingModel>().SetWeightIndex(i);
		}
	}

    public int GetWeightCount()
    {
        return GetAxis().weights.Count;
    }

    public Weight GetWeight(int index)
    {
        //Debug.Log("Error Point2 : " + index);
        return GetAxis().weights[index];
    }

	public int GetAxisIndex()
	{
		return axisIndex;
	}


	public void changeColor(Color _color)
	{
		axisList[axisIndex].GetComponent<MeshRenderer>().material.color = _color;
		Axis temp = AxisDataManager.Instance().GetAxis(AxisSetting.index);
		temp.color = _color;
		AxisDataManager.Instance().ChangeAxisAt(AxisSetting.index, temp);
	}
}
