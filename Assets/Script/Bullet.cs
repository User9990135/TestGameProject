using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerC Player;
  
    private Rigidbody2D rb;
    public float Damage;
    float speed;
    public float rotateSpeed = 0.5f;
    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerC>();
        rb = GetComponent<Rigidbody2D>();
        speed = 0.5f;
        //target = Player.enemies[0].transform;
        Damage =Player.Mage*1 + 10;

        Destroy(gameObject,4f);
    }

    // Update is called once per frame
    void Update()
    {
      //  target = Player.enemiesTransform.transform;
      
      
            if (!target)
            {
                GetTarget();
            }
            else
            {
                RotateTowardsTarget();
            }
        
    }
    private void GetTarget()
    {
        try
        {
            GameObject EnemyT = Player.enemies[0];
            target = EnemyT.transform;
        }
        catch (System.Exception)
        {
            Debug.Log("xx");
        }
       // target = GameObject.FindGameObjectWithTag("Player").transform;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamaged(Damage);
            Destroy(gameObject);
            
        }
    }

}
