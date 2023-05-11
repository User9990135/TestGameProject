using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager GM = new GameManager();
    GameObject GMa;

    public float Hp;
    [SerializeField] bool Boss;
    public Transform target;
    [SerializeField] private float speed;
    public float rotateSpeed ;
    public float Atk;
    private Rigidbody2D rb;

    private void Start()
    {
        GMa = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody2D>();
        Hp = Hp + 0.2f * GM.clearmap;
        Atk = Atk + 0.2f * GM.clearmap;
       
        rotateSpeed = Random.Range(0.025f, 0.015f);
        if (Boss == true)
        {
            Atk = 5 * 0.2f * GM.clearmap;
            Hp = 1000 * 0.2f * GM.clearmap;
        }
       

    }


    void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }
    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }
    private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void GetDamaged(float Damage)
    {
        
        Hp = Hp - Damage;

        if (Hp < 0)
        {
            GMa.GetComponent<GameManager>().MonsterKill();
            Debug.Log("적을처치했습니다.");
            Destroy(gameObject);

        }

    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {


            Debug.Log("플레이어에게 데미지를 입힌다!" + Atk);
            
            collision.GetComponent<PlayerC>().Damaged((int)Atk);

        }
    }

   
}
