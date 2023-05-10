using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class PlayerC : MonoBehaviour
{
    public ShaderTest shaderTest;
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

    // �� �迭 üũ

   // public List<Enemy> positionList;
    public Transform player;
    public bool F = false;
    public GameObject[] enemies;

    //PlayerMove//
    public float Speed;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    public bool GameStart = false; // ���� ��ŸƮ ��ư�� ������ Ʈ��� �ٲ�� �����մϴ�!
    void Start()
    {
        shaderTest.UpdateOutline(false);
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

        Hp = MaxHp;
        Attack_Speed = 3 - 0.02f * Mage;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameStart == true)
        {
          
            PlayerMove();

            //         enemiesTransform = enemies[0].transform;


             if (Input.GetKeyUp(KeyCode.Q))
             {
                if(Q_count == false)
                {
                    
                    Invoke("Q_Shot", 1f / Attack_Speed);
                    
                }
                
                
             }
        }
    }
    public bool Q_count = false;
    public void Q_Shot()
    {
        Q_count = true;
        shaderTest.UpdateOutline(true);

        Invoke("Q_CountDown", 5f / Attack_Speed);
        EnemyCheck();
        Invoke("ShaderQ", 0.1f);
     
        
    }
    public bool W_count = false;
    public void W_Shot()
    {
        W_count = true;
        shaderTest.UpdateOutline(true);

        Invoke("W_CountDown", 5f / Attack_Speed);
        EnemyCheck();
        Invoke("ShaderW", 0.1f);


    }
    public void ShaderQ()
    {
        A = 0;
        GM.GetComponent<GameManager>().Close_skill(A);
        shaderTest.UpdateOutline(false);
        Invoke("BulletSpawn", 0.0f); 

    }
    public void Q_CountDown()
    {
        GM.GetComponent<GameManager>().Open_skill(A);
        Q_count = false;
       
    }
    public GameObject[] Magic;
    public int A = 0;
    void BulletSpawn()
    {
        shaderTest.UpdateOutline(false);
        Bullet Ammo = Instantiate(Magic[A]).GetComponent<Bullet>();
        Ammo.transform.position = new Vector3(transform.position.x, transform.position.y);

    }

    private void FixedUpdate()
    {
        
        if (GameStart == true)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
       // StartCoroutine("Shot");
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
            Debug.Log("�� ���ƴ�" + Hp);
            
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



    public void EnemyCheck()
    {
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (F == false)
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
            enemies[i].transform.SetSiblingIndex(i); // �迭 ������� transform�� SiblingIndex�� �����Ͽ� �迭 ���ʿ� ��ġ�ϵ��� ��
            
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

