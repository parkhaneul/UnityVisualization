using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//property의 상위 오브젝트여야함(listcontent)
public class AxisViewModel : MonoBehaviour {
	
	public Dropdown dropdown;
	public InputField InputText;
	public GameObject propertyController;

	private static AxisViewModel _instance;
	private int axisIndex;
	private List<WeightSettingModel> propertyControllers = new List<WeightSettingModel>();

	public static AxisViewModel Instance()
	{
		if (_instance == null)
		{
			_instance = new AxisViewModel();
		}
		return _instance;
	}

	private void Start()
	{
		AxisDataManager.Instance().AddAxis();
		axisIndex = 0;
	}

	public void OnClickPlusButton()
	{
		//데이터 초기화 이름
		AxisDataManager.Instance().AddAxis();
		dropdown.options.Add(new Dropdown.OptionData("new Axis"));
	}

	public void OnClickMinusButton()
	{
		if (axisIndex > 0) {
			AxisDataManager.Instance().RemoveAxisAt(axisIndex);
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
//		temp.name = InputText.text;
		SetAxis(temp);
		dropdown.options[dropdown.value].text = InputText.text;
	}

	public void OnClickAddWeightButton()
	{
		AddWeight();
		AddWeightSettingObject(GetWeight(GetWeightCount() - 1));
	}

	public void OnChangeAxisIndex()
    {
        axisIndex = dropdown.value;
		InputText.text = GetAxis().name;

		foreach(Transform p in this.transform)
		{
			GameObject.Destroy(p.gameObject);
		}
		propertyControllers.Clear();
		
		for (int i = 0; i < GetAxis().weights.Count; i++)
		{
			Debug.Log(i);
			AddWeightSettingObject(GetWeight(i));
		}
    }

	

	private void AddWeightSettingObject(Weight w)
	{
		Debug.Log(w.propertyIndex + "::" + w.weight);
		GameObject g = GameObject.Instantiate(propertyController);
		g.transform.SetParent(this.transform);
		g.transform.localScale = Vector3.one;
		g.GetComponent<WeightSettingModel>().SetWeightIndex(propertyControllers.Count);
		g.GetComponent<WeightSettingModel>().axisViewModel = this;
		propertyControllers.Add(g.GetComponent<WeightSettingModel>());
		g.GetComponent<WeightSettingModel>().dropdown.value = w.propertyIndex;
		g.GetComponent<WeightSettingModel>().slider.value = w.weight;
		g.GetComponent<WeightSettingModel>().value.text = w.weight.ToString();
	}

    public void SetAxis(Axis _axis)
    {
		Debug.Log("SetAxis" + axisIndex);
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
		Debug.Log("SetWeight" + index);
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

		GameObject.Destroy(propertyControllers[_index].gameObject);
		propertyControllers.RemoveAt(_index);

		for(int i = 0; i < propertyControllers.Count; i++) {
			propertyControllers[i].GetComponent<WeightSettingModel>().SetWeightIndex(i);
		}
	}

    public int GetWeightCount()
    {
        return GetAxis().weights.Count;
    }

    public Weight GetWeight(int index)
    {
        return GetAxis().weights[index];
    }
}
