using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myInputField : MonoBehaviour
{
    private string input;


    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
    }
}

