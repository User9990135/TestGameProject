using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerC Player = new PlayerC();
  
    private Rigidbody2D rb;
    public int Damage;
    float speed;
    public float rotateSpeed = 0.5f;
    public Transform target;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
 
        rb = GetComponent<Rigidbody2D>();
        speed = 0.5f;
        GetTarget();
        Damage =Player.Mage*1 + 1;
        Destroy(gameObject,9f);
    }

    // Update is called once per frame
    void Update()
    {
 
        
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
       target = Player.enemies[0].transform;
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
