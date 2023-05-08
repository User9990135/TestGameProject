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

    // ����
    public int Die;


    public int ClearMap;
    //�� ��ø ����
    public int Level;
    public int H;
    public int D;
    public int I;
    public int Hp;
    public int Mana;
    public bool Wch;
    public int Coin;
    public void GetData()
    {
        Debug.Log("Die : " + Die);
   //     Debug.Log("W : " + W);
    //    Debug.Log("B : " + B);
       
        Debug.Log("ClearMap : " + ClearMap);
        //
        Debug.Log("Level : " + Level);
        Debug.Log("H : " + H);
        Debug.Log("D : " + D);
        Debug.Log("I : " + I);
        Debug.Log("Hp : " + Hp);
        Debug.Log("Mana : " + Mana);
        Debug.Log("Coin : " + Coin);
    }
}


public class GameManager : MonoBehaviour
{
    //���� �ý����Դϴ� ���ӽý����� �ý��� �޽���â�� �÷��̾��� �������� �ְ� �Է��Ѱ��� �޾� ������ �����մϴ�


   

    public int die = 0;
   
    public int clearmap;
    public int level =1;
    public int h =1;
    public int d =1;
    public int  i =1;
    public int hp = 100;
    public int mana = 10;
    public int coin = 0;
    public void SaveData()  //���Ӽ��̺�!
    {
        MyData mydata = new MyData();
        mydata.Die = die;
      
        mydata.ClearMap = clearmap;
        mydata.Level = level;
        mydata.H = h;
        mydata.D = d;
        mydata.I = i;
        mydata.Hp = hp;
        mydata.Mana = mana;
        mydata.Coin = coin;
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

    }
 

}
