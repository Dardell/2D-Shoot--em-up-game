using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Joystick joystick;
    public Button attackButton;
    public float healthMax;
    public float health;
    public float speed;
    public float attackSpeed;
    public float bulletSpeed;
    Vector2 clickPosition;
    bool moving;
    void Start()
    {
        Application.targetFrameRate = 120;
        speed = 4f;
        attackSpeed = 0.5f;
        bulletSpeed = 7f;
        healthMax = 100;
        health = 100;
    }

    private void FixedUpdate()
    {
        MovementJoystick();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy_01 enemy_01 = collision.gameObject.GetComponent<Enemy_01>();
            
            health -= enemy_01.attack;
        }
    }

    public void MovementTouch(){
        if (Input.GetMouseButton(0)){
        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, clickPosition, step);

        if (rb.position.x > clickPosition.x)
        {
        transform.localScale = new Vector3(-1,1,1);
        }
        else transform.localScale = new Vector3(1,1,1);
        }
    }

    public void MovementJoystick(){
        if (joystick.Horizontal != 0 || joystick.Vertical != 0){
        Vector2 direction = new Vector2 (joystick.Horizontal, joystick.Vertical);
        direction.Normalize();
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else {
        Vector2 directionPC = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        directionPC.Normalize();
        rb.MovePosition(rb.position + directionPC * speed * Time.fixedDeltaTime);
        }

        if (joystick.Horizontal < 0 || Input.GetAxisRaw("Horizontal") < 0){
        transform.localScale = new Vector3(-1,1,1); //setSpriteToLeft
        }
        else if (joystick.Horizontal > 0 || Input.GetAxisRaw("Horizontal") > 0){
        transform.localScale = new Vector3(1,1,1); //setSpriteToRight
        }
    }
}

