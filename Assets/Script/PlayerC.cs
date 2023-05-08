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
    public bool GameStart = false; // ���� ��ŸƮ ��ư�� ������ Ʈ��� �ٲ�� �����մϴ�!
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
        Debug.Log(Dex + "��ø");
        MaxHp = GM.GetComponent<GameManager>().hp * 10 + 100;
        Debug.Log(MaxHp + "�����");
        MaxMp = GM.GetComponent<GameManager>().mana * 1 + 10;
        Debug.Log(MaxMp + "����");
        Atk = GM.GetComponent<GameManager>().h;
        Debug.Log(Atk + "��");
        Mage = GM.GetComponent<GameManager>().i;
        Debug.Log(Mage + "����");
        Speed = 0.5f + 0.1f * (GM.GetComponent<GameManager>().d);
        Debug.Log(Speed + "�̵��ӵ�");
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
