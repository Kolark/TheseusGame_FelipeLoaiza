using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SignTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("0" + Math.Sign(0));
        Debug.Log("0" + Mathf.Sign(0));
    }

    private void OnValidate()
    {
        Debug.Log("0 system" + System.Math.Sign(0));
        Debug.Log("0 unity" + Mathf.Sign(0));
    }
}
