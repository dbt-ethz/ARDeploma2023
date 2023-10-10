using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class ChangeDisplay : MonoBehaviour
{
    public TMP_Dropdown dropDown;
    ARTrackedImageManager m_TrackedImageManager;

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        dropDown.onValueChanged.AddListener(delegate { ClickChangeDisplay(dropDown); });
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        //Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
       // Application.logMessageReceived -= HandleLog;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            trackedImage.transform.localPosition = new Vector3(trackedImage.transform.localPosition.x, 0.005f, trackedImage.transform.localPosition.z);
        }
    }
    public void ClickChangeDisplay(TMP_Dropdown myDropDown)
    {
        TextManager.Instance.debugText.text = "change mats";
        string name = myDropDown.value.ToString();
        TextManager.Instance.debugText.text = name;
        Material mat = (Material)Resources.Load(name);
        GameObject parent = GameObject.FindGameObjectWithTag("Model");
        if (parent != null)
        {
            GameObject terrain = parent.transform.GetChild(0).gameObject;
            terrain.GetComponent<MeshRenderer>().material = mat;
        }
        else TextManager.Instance.debugText.text = "didnt find material";
    }
    public void ClickChangeMat(Material mat)
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Model");
        if (parent != null)
        {
            GameObject terrain = parent.transform.GetChild(0).gameObject;
            terrain.GetComponent<MeshRenderer>().material = mat;
        }
        else TextManager.Instance.debugText.text =  "didnt find terrain";
    }
    public void OnOffRoad()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Model");
        if (parent != null)
        {
            GameObject road = parent.transform.GetChild(1).gameObject;
            road.SetActive(!road.activeSelf);
        }
        else TextManager.Instance.debugText.text = "didnt find terrain";
    }
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        //debugText.text += logString + "\n";
    }
}
