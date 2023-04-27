using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myInputField : MonoBehaviour
{
    private string input;
    public string A_input;
    ////GameManager////
    public GameManager gameManager;
    ////////////////////
    

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
        A_input = input;
        
        gameManager.TextInput(input);
    }
}

