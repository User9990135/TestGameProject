using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExcelReader : MonoBehaviour
{
    public TextAsset textAssetdata;
    //¤¤ exceldata  input ok?
    public string TextLog;
    public string A_;
    public string Name;
    public string C_text;
    public string Answer;


    ////GameManager////
    public GameManager gameManager;
    //////////////////

    void Start()
    {
       
    }
    
    public void Search(string Text_log)
    {
        TextLog = Text_log;
        string[] data = textAssetdata.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
        
        for(int i = 0; i < data.Length; i++)
        {
            if(TextLog == data[i])
            {
                Name= data[i + 1];
                C_text = data[i + 2];
                Answer= data[i + 3];

                A_ = string.Format("{0}\n{1}\n{2}",Name, C_text,Answer);
                gameManager.TextIn(A_);
            }
        }

        
    }
    void Update()
    {
        
    }
}
