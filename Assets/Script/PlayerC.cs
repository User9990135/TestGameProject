using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

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

    public float Attack_Speed;
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

        Hp = MaxHp;
        Attack_Speed = 3 - 0.02f * Mage;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameStart == true)
        {
            PlayerMove();
            EnemyCheck();


        }
    }

    

    public GameObject Magic;
    void BulletSpawn()
    {

        Bullet Ammo = Instantiate(Magic).GetComponent<Bullet>();
        Ammo.transform.position = new Vector3(transform.position.x, transform.position.y);

    }

    IEnumerator Shot()
    {
        while (true)
        {

            //shaderTest.UpdateOutline(true);
            yield return new WaitForSeconds(1f / Attack_Speed);
            //shaderTest.UpdateOutline(false);
            yield return new WaitForSeconds(1f / Attack_Speed);
            BulletSpawn();
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
        StartCoroutine("Shot");
        GM.GetComponent<GameManager>().CloseMenu();
        
       
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

    public bool Hurt = false;
    public int Damaged(int Enemy_atk)
    {
       
        if (Hurt == false)
        {
            
            Hp = Hp - Enemy_atk;
            Debug.Log("윽 다쳤다" + Hp);
            
            DamagedWait();

        }

        if (Hp < 0)
        {

            Destroy(gameObject);

        }

        return Hp;
    }
    IEnumerator DamagedWait()
    {
        int countTime = 0;
        Hurt = true;
        while (countTime < 3)
        {
            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        Hurt = false;
        yield return null;

    }
    public List<Enemy> positionList;
    public Transform player;
    public bool F = false;
    public GameObject[] enemies;
    public void EnemyCheck()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (F = false)
        {
            enemies = enemies.OrderBy(enemy => Vector3.Distance(enemy.transform.position, player.position)).ToArray();
        }
        else
        {
            enemies = enemies.OrderByDescending(enemy => Vector3.Distance(enemy.transform.position, player.position)).ToArray();
        }

        // enemies = enemies.OrderByDescending(enemy => Vector3.Distance(enemy.transform.position, player.position)).ToArray();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.SetSiblingIndex(i); // 배열 순서대로 transform의 SiblingIndex를 변경하여 배열 앞쪽에 위치하도록 함
            
        }
    }

    public void EnemyLine()
    {
        bool a = true;

        if (a)
        {
            F = false;
            a = false;
        }
        else
        {
            F = true;
            a = true;
        }

    }

}

