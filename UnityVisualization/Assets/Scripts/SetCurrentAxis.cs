using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetCurrentAxis : MonoBehaviour
{
    public DataManagement manager;
    public Dropdown dropdown;
    private int currentIndex = 0;

    public void addValue(string str = "new Axis")
    {
        var newAxis = new Axis();
        manager.axisArray.Add(newAxis);
        dropdown.options.Add(new Dropdown.OptionData(str));
    }

    public void deleteValue()
    {
        var value = dropdown.value;
        manager.axisArray.RemoveAt(value);
        dropdown.options.RemoveAt(value);
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    public void changeAxis(int index)
    {
        currentIndex = index;
    }

    public int getListCount()
    {
        return manager.axisArray.Count;
    }
}
