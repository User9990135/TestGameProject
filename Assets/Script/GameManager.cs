using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using System.Numerics;
using System.IO;
using UnityEngine.SceneManagement;


[Serializable]
public class MyData
{

    // 게임
    public int Die;
//   public int W; //명성
 //   public int B; //악명
    public string PlayerName;
    public int ClearMap;
    //힘 민첩 지능
    public int Level;
    public int H;
    public int D;
    public int I;
    public int Hp;
    public int Mana;


    public void GetData()
    {
        Debug.Log("Die : " + Die);
   //     Debug.Log("W : " + W);
    //    Debug.Log("B : " + B);
        Debug.Log("PlayerName : " + PlayerName);
        Debug.Log("ClearMap : " + ClearMap);
        //
        Debug.Log("Level : " + Level);
        Debug.Log("H : " + H);
        Debug.Log("D : " + D);
        Debug.Log("I : " + I);
        Debug.Log("Hp : " + Hp);
        Debug.Log("Mana : " + Mana);
    }
}

public enum TextKeyWord
{
    Mstart, //메인 스타트
    start, //게임스타트
    bettle,
    setting,
    error,
    hello,
    exit
}
public enum GameSceneType
{
    Story,
    Battle,
    Main,
    Setting
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
    // savedata
    public int die;
    public string playername = "아무개";
    public int clearmap;
    public int level;
    public int h ;
    public int d ;
    public int  i;
    public int hp;
    public int mana;

    public void SaveData()
    {
        MyData mydata = new MyData();
        mydata.Die = die;
        mydata.PlayerName = playername.ToString();
        mydata.ClearMap = clearmap;
        mydata.Level = level;
        mydata.H = h;
        mydata.D = d;
        mydata.I = i;
        mydata.Hp = hp;
        mydata.Mana = mana;

        string str = JsonUtility.ToJson(mydata);

        Debug.Log(str);

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json",
            JsonUtility.ToJson(mydata));
        MyData data2 = JsonUtility.FromJson<MyData>(str);
        data2.GetData();

    }


    public void GameLoad(string jsonData)
    {
        MyData myData = JsonUtility.FromJson<MyData>(jsonData);
        myData.GetData();
        //money = BigInteger.Parse(myData.Money);
        //weaponLevel = myData.WeaponLevel;
        //AtkSpd = (float)myData.Atkspd;
        //stageLevel = myData.StageLevel;
        die = myData.Die;
    }
    //@@@@@@@@@@@@@@@@@@@@@@@@//

    void Start()
    {
        //save try
        try
        {
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            // Debug.LogError("test 11");
            GameLoad(jsonData);// Debug.LogError("test 12");
        }
        catch
        {
            // Debug.LogError("test 1");
            SaveData();
        }
        //
        Systemtextprint(TextKeyWord.Mstart);

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
            case TextKeyWord.Mstart:
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
            case TextKeyWord.start:
                Systemtext.text = string.Empty;
                SystemStr = "\n게임을 시작합니다.";
                GameSaveCheck();
                break;

        }

    }
    public void GameSaveCheck()
    {
        
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
