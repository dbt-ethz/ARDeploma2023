using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDisplay : MonoBehaviour
{
    private void Start()
    {

    }
    public void ClickChangeMat(Material mat)
    {
        //GameObject terrain = GameObject.Find()
        GetComponent<MeshRenderer>().material = mat;
    }
}
