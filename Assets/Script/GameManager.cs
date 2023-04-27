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
    Setting,
    choice  //
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

    ////ExcelReader////
    public ExcelReader excelReader;
    //////////////////

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
                TextIn("[start] [setting] [exit]");
                break;
            case TextKeyWord.error: // error
                TextIn("오류가 발생했습니다");
                break;
            case TextKeyWord.exit: //game exit
                TextIn("게임을 종료합니다.");
                //게임종료하기 
                break;
            case TextKeyWord.hello: //game exit
                TextIn("안녕하세요?");
                break;
            case TextKeyWord.start:
                TextIn("게임을 시작합니다.");
                //스테이지 선택 -> 스테이지 시작
                
                
                break;
            case TextKeyWord.setting:
                TextIn("시스템 설정으로 이동합니다.");
              
                //시스템 설정 보이게하기
                break;
                

        }

    }
    public int GameType = 0;
    public void StageCheck(string D)
    {
        
        //챕터 선택 하는 스크립트입니다.
        TextIn("원하시는 챕터를 선택하세요");
        GameType = (int)GameSceneType.choice;
        if(GameType == 4)
        {
            TextIn("선택하실수 있는 최대 스테이지는 " +(clearmap +1)+ "입니다");
            string c = myInputfield.A_input;
            excelReader.Search(c);
        }
        
    }
    public int c = 0; //텍스트 출력수입니다 많으면 사라집니다.
    public string TextIn(string A)
    {
         c ++;
        if (c > 1)
        {
            Systemtext.text = string.Empty;
            c = 0;
        }
        
        SystemStr = "\n"+A;
        return A;
    } //텍스트를 넣어서 출력합니다

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
