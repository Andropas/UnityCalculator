using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HidePortrait : UIBehaviour
{

    public GameObject funcPanel;
    public GameObject resultPanel;
    public Text label;

    protected override void OnRectTransformDimensionsChange()
    {   
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            funcPanel.SetActive(false);
            resultPanel.SetActive(true);
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            funcPanel.SetActive(true);
            resultPanel.SetActive(false);
        }
    }
    void Start()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            funcPanel.SetActive(false);
            resultPanel.SetActive(true);
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            funcPanel.SetActive(true);
            resultPanel.SetActive(false);
        }
    }
}
