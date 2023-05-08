using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerC : MonoBehaviour
{
    GameObject GM;
    //EnemySpawn enemyspawn = new EnemySpawn();
    GameObject ES;
    public int Dex;       // speed = 10+ 0.1*Dex
    public int MaxHp;     // 100+10*H
    public int Hp;
    public int Atk;    // S
    public string PlayerName;
    public int Mage;  // I 
    public int Mp;
    public int MaxMp;

    //PlayerMove//
    public float Speed;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    public bool GameStart = false; // 게임 스타트 버튼을 누를시 트루로 바뀌고 시작합니다!
    void Start()
    {
        GM = GameObject.Find("GameManager");
        ES = GameObject.Find("EnemySpawner");
        rb = GetComponent<Rigidbody2D>();
        Invoke("StatSet", 0.1f);
    }

    void StatSet()
    {
        
        Dex = GM.GetComponent<GameManager>().d;
        Debug.Log(Dex + "민첩");
        MaxHp = GM.GetComponent<GameManager>().hp * 10 + 100;
        Debug.Log(MaxHp + "생명력");
        MaxMp = GM.GetComponent<GameManager>().mana * 1 + 10;
        Debug.Log(MaxMp + "마나");
        Atk = GM.GetComponent<GameManager>().h;
        Debug.Log(Atk + "힘");
        Mage = GM.GetComponent<GameManager>().i;
        Debug.Log(Mage + "마력");
        Speed = 0.5f + 0.1f * (GM.GetComponent<GameManager>().d);
        Debug.Log(Speed + "이동속도");
    }
    // Update is called once per frame
    void Update()
    {
        if (GameStart == true)
        {
            PlayerMove();
        }
    }
    private void FixedUpdate()
    {
        if (GameStart == true)
        {
            rb.velocity = new Vector2(playerDirection.x * Speed, playerDirection.y * Speed);
        }
    }
    public void PlayerMove()
    {
        float dX = Input.GetAxisRaw("Horizontal");
        float dY = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(dX, dY).normalized;
    }

    
    public void GameStart_Bt()
    {
        
        GameStart = true;
       // ES.GetComponent<EnemysSpawn>().SpawnA();
        StatSet();
    }
    public bool a = false;
    public void UI_StatBt()
    {
        if (a != true)
        {
            Stat_ui.gameObject.SetActive(true);
            StatSet();
            UI_Stat();
            a = true;
        }
        else
        {

            Stat_ui.gameObject.SetActive(false);
            StatSet();
            UI_Stat();
            a = false;
        }
       
        
        
    }
    public GameObject Stat_ui;
    public Text Stat_text;

    void UI_Stat()
    {
        Stat_text.text = "Hp : " + MaxHp + "\nMp : " + MaxMp + "\nDex : " + (Dex + 1) + "\nInt : " + (Mage + 1) + "\nStr : " + (Atk + 1) + "\nSpeed : " + Speed;

    }

}
