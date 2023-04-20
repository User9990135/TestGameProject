using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public enum TextKeyWord
{
    start,
    bettle,
    setting,
    error,
    hello,
    exit
}
public class GameManager : MonoBehaviour
{
    //게임 시스템입니다 게임시스템은 시스템 메시지창에 플레이어의 선택지를 주고 입력한값을 받아 선택을 진행합니다
    public Text Systemtext;
    public string SystemStr;
    public string SystemNum = "";
    public bool CanInput = false;

    ////myInputField////
    public myInputField myInputfield;
    ////////////////////
    public TextKeyWord textKeyWord;
 
    void Start()
    {
        
        Systemtextprint(TextKeyWord.start);

    }
    public void Update()
    {
    
    }

    public void TextInput(string Num)
    {
        bool A = false;
       
        A = Enum.IsDefined(typeof(TextKeyWord), Num);
        
        if (!A) 
        {
            SystemMessage(TextKeyWord.error);
        }
        else
        {
            textKeyWord = (TextKeyWord)System.Enum.Parse(typeof(TextKeyWord), Num);
            Systemtextprint(textKeyWord);
        }
    }

    public void ABCheck()
    {
        //string[] KeyWord = {}
    }
    public void Systemtextprint(TextKeyWord Snum)
    {
        SystemMessage(Snum);
        //Systemtext.text = SystemStr;
        StartCoroutine(Typing(SystemStr));

    }

    public void SystemMessage(TextKeyWord Num)
    {
        switch (Num)
        {
            case TextKeyWord.start:
                Systemtext.text = string.Empty;
                SystemStr = "반갑습니다\n저는 당신의 게임 시스템 입니다\n[start] [setting] [exit]";
                break;
            case TextKeyWord.error: // error
                Systemtext.text = string.Empty;
                SystemStr = "\n에러가 발생하였습니다 전 선택지로 이동합니다.";
                break;
            case TextKeyWord.exit: //game exit
                Systemtext.text = string.Empty;
                SystemStr = "\n게임을 종료합니다 수고하셨습니다.";
                break;
            case TextKeyWord.hello: //game exit
                Systemtext.text = string.Empty;
                SystemStr = "\n안녕하세요.";
                break;
        }

    }

    IEnumerator Typing(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            Systemtext.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

   
    // Update is called once per frame

}
