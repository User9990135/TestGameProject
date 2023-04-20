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
    //���� �ý����Դϴ� ���ӽý����� �ý��� �޽���â�� �÷��̾��� �������� �ְ� �Է��Ѱ��� �޾� ������ �����մϴ�
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
                SystemStr = "�ݰ����ϴ�\n���� ����� ���� �ý��� �Դϴ�\n[start] [setting] [exit]";
                break;
            case TextKeyWord.error: // error
                Systemtext.text = string.Empty;
                SystemStr = "\n������ �߻��Ͽ����ϴ� �� �������� �̵��մϴ�.";
                break;
            case TextKeyWord.exit: //game exit
                Systemtext.text = string.Empty;
                SystemStr = "\n������ �����մϴ� �����ϼ̽��ϴ�.";
                break;
            case TextKeyWord.hello: //game exit
                Systemtext.text = string.Empty;
                SystemStr = "\n�ȳ��ϼ���.";
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
