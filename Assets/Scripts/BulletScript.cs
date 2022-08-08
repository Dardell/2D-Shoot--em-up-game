using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float penetration;
    float started_flying;
    void Start()
    {
        started_flying = Time.time;
        penetration = 1;
    }

    void Update()
    {
        if (Time.time - started_flying > 10){
            Destroy(gameObject);
        }
        if (penetration <= 0){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            penetration -= 1; 
        }
    }
}
