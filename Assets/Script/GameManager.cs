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
    

    public int ClearMap;
    //힘 민첩 지능
    public int Level;
    public int H;
    public int D;
    public int I;
    public int Hp;
    public bool Wch;
    public int Coin;
    public void GetData()
    {
   //     Debug.Log("W : " + W);
    //    Debug.Log("B : " + B);
       
        Debug.Log("ClearMap : " + ClearMap);
        //
        Debug.Log("Level : " + Level);
        Debug.Log("H : " + H);
        Debug.Log("D : " + D);
        Debug.Log("I : " + I);
        Debug.Log("Hp : " + Hp);
 
        Debug.Log("Coin : " + Coin);
    }
}


public class GameManager : MonoBehaviour
{
    //게임 시스템입니다 게임시스템은 시스템 메시지창에 플레이어의 선택지를 주고 입력한값을 받아 선택을 진행합니다
    public GameObject Game_1;
    public GameObject Enemy_Spawn;
    PlayerC Player;
    public GameObject Ingame_UI;
    public GameObject[] img_skill;


    public int DexStack;
    public int StrStack;
    public int IntStack;
    public int NeedCoin_1;
    public int NeedCoin_2;
    public int NeedCoin_3;

    public Text DexNeedCoin_text;
    public Text StrNeedCoin_text;
    public Text IntNeedCoin_text;
    public Text Coin_text;
    public Text Stage_text;


    public bool GameUI_bool = false;
    public bool GameStart;
   
    public int clearmap;
    public int level =1;
    public int h =1;
    public int d =1;
    public int  i =1;
    public int hp = 100;
    public int mana = 10;
    public int coin = 0;


    public void SaveData()  //게임세이브!
    {
        MyData mydata = new MyData();
 
      
        mydata.ClearMap = clearmap;
        mydata.Level = level;
        mydata.H = h;
        mydata.D = d;
        mydata.I = i;
        mydata.Hp = hp;

        mydata.Coin = coin;
        string str = JsonUtility.ToJson(mydata);

        Debug.Log(str);

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json",
            JsonUtility.ToJson(mydata));
        MyData data2 = JsonUtility.FromJson<MyData>(str);
        data2.GetData();

    }


    public void Close_skill(int A)
    {
        switch (A)
        {
            case 0:
                img_skill[A].SetActive(false);
                break;

            case 1:
                img_skill[A].SetActive(false);
                break;
        }
    }
    public void Open_skill(int A)
    {
        switch (A)
        {
            case 0:
                img_skill[A].SetActive(true);
                break;

            case 1:
                img_skill[A].SetActive(true);
                break;
        }
    }
    public void CloseMenu()
    {
        Game_1.gameObject.SetActive(false);
        Enemy_Spawn.GetComponent<EnemySpawn>().SpawnA();
    }
   
    public void Ingame_bt()
    {
        clearmap = 0;
        if(GameUI_bool == false)
        {
            Ingame_UI.gameObject.SetActive(true);
            GameUI_bool = true;
        }
        else if(GameUI_bool == true)
        {
            Ingame_UI.gameObject.SetActive(false);
            GameUI_bool = false;
        }
        
    }
    public void BossKill()
    {
        coin += 1+(clearmap*1);
        clearmap += 1;
        SetStage_text();

    }
    public void GameEnd()
    {
        string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
        coin += clearmap * 1;
        GameStart = false;
        clearmap = 0;
        SetStage_text();
        SetCoin_text();
        SaveData();
        GameLoad(jsonData);
        MainReset();

    }
    public void MainReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetCoin_text()
    {
        Coin_text.text=("Coin : " + coin);
    }

    public void SetStage_text()
    {
        Stage_text.text = ("Stage : " + clearmap);
    }

    public void SetCoinNeed()
    {
       
        Player.GetComponent<PlayerC>().UI_Stat(); 
        NeedCoin_1 = (DexStack * 1) + 1;
        NeedCoin_2 = (IntStack * 1) + 1;
        NeedCoin_3 = (StrStack * 1) + 1;
        DexNeedCoin_text.text = ""+NeedCoin_1;
        StrNeedCoin_text.text = "" + NeedCoin_2;
        IntNeedCoin_text.text = "" + NeedCoin_3;
    }
    public void DexUp()
    {

      
        if (coin >= NeedCoin_1)
        {
            coin -= NeedCoin_1;
            d += 1;
            DexStack += 1;
            Player.GetComponent<PlayerC>().StatSet();
            SetCoin_text();
        }

    }

    public void IntUp()
    {

      
        if (coin >= NeedCoin_2)
        {
            coin -= NeedCoin_2;
            i += 1;
            IntStack += 1;
            Player.GetComponent<PlayerC>().StatSet();
            SetCoin_text();
        }
    }
    public void StrUp()
    {

       
        if (coin >= NeedCoin_3)
        {
            coin -= NeedCoin_3;
            h += 1;
            StrStack += 1;
            Player.GetComponent<PlayerC>().StatSet();
            SetCoin_text();

        }
    }


    public void GameLoad(string jsonData)
    {
            DexStack = d;
            StrStack = h;
            IntStack = i;
        
        MyData myData = JsonUtility.FromJson<MyData>(jsonData);
        myData.GetData();
        //money = BigInteger.Parse(myData.Money);
        //weaponLevel = myData.WeaponLevel;
        //AtkSpd = (float)myData.Atkspd;
        //stageLevel = myData.StageLevel;

    }
    //@@@@@@@@@@@@@@@@@@@@@@@@//

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerC>();
        try
        {
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            // Debug.LogError("test 11");
            GameLoad(jsonData);// Debug.LogError("test 12");
            SetCoin_text();
           
        }
        catch
        {
            // Debug.LogError("test 1");
            SaveData();
            SetCoin_text();
           
        }

    }

    public void Update()
    {
        
    }
   
}
