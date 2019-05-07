using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    public List<Axis> axisArray = new List<Axis>();
    public SetCurrentAxis currentAxis;
    public GameObject visualizationSpace;
     public GameObject visualizationAxis;

    private List<GameObject> vAxisArray = new List<GameObject>();

    public void addAxis(Axis _axis)
    {
        axisArray.Add(_axis);
        var temp = Instantiate(visualizationAxis, visualizationSpace.transform, true);
        temp.SetActive(true);
        vAxisArray.Add(temp);
    }

    public void removeAxis(int index)
    {
        axisArray.RemoveAt(index);
        var temp = vAxisArray[index];
        vAxisArray.RemoveAt(index);
        Destroy(temp);
    }

    public List<Axis> getAxisArray() {
        for(int i = 0; i < axisArray.Count; i++)
        {
            axisArray[i].setVector(vAxisArray[i].transform.position);
            //setColor
        }
        return axisArray;
    }
}