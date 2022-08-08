using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01 : MonoBehaviour
{
    public float speed;
    public float max_health;
    public float health;
    public float attack;
    GameObject player;
    Vector2 direction;
    void Start()
    {
        speed = 1;
        player = GameObject.FindWithTag("Player");
        max_health = 2;
        health = 2;
        attack = 20;
    }
    void Update()
    {
        MoveTowardsPlayer();
        if (health <=0){
            Destroy(gameObject);
        }
    }

    void MoveTowardsPlayer(){
        direction = player.transform.position;
        //direction.Normalize();
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        //rigidbody.velocity = rigidbody.velocity * -1;
        health -= 1;
        Debug.Log("COLLISION!!!");
        Debug.Log(collision.gameObject.name);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
    }
    
}
