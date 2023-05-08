using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour
{
    GameObject GM;

    public int Dex;       // speed = 10+ 0.1*Dex
    public int MaxHp;     // 100+10*H
    public int Hp;
    public int Atk;    // S

    public int Mage;  // I 
    public int Mp;

   

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager");
        Invoke("StatSet", 0.1f);
    }

    void StatSet()
    {
        Dex = GM.GetComponent<GameManager>().d;
        Debug.Log(Dex + "¹ÎÃ¸");
        MaxHp = GM.GetComponent<GameManager>().hp;
        Debug.Log(MaxHp + "»ý¸í·Â");
        Atk = GM.GetComponent<GameManager>().h;
        Debug.Log(Atk + "Èû");
        Mage = GM.GetComponent<GameManager>().i;
        Debug.Log(Atk + "¸¶·Â");
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
