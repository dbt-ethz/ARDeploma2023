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
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            trackedImage.transform.localRotation = Quaternion.Euler(trackedImage.transform.localRotation.x, 0, trackedImage.transform.localRotation.z);
        }
    }
    public void ClickChangeDisplay(TMP_Dropdown myDropDown)
    {
        string name = myDropDown.options[myDropDown.value].text;
        Material mat = Resources.Load(name, typeof(Material)) as Material;
        GameObject parent = GameObject.FindGameObjectWithTag("Model");
        if (parent != null)
        {
            GameObject terrain = parent.transform.GetChild(0).gameObject;
            terrain.GetComponent<MeshRenderer>().material = mat;
        }
        else TextManager.Instance.debugText.text = "didnt find material";

        if(parent.transform.childCount > 0)
        {
            Destroy(parent.transform.GetChild(1).gameObject);
        }
        Debug.Log(name);
        GameObject buildings = Resources.Load<GameObject>(name);
        GameObject instGo = Instantiate(buildings, parent.transform);
        instGo.transform.localPosition = new Vector3(0, 0, 0);
        instGo.transform.localRotation = Quaternion.identity;
    }
}
