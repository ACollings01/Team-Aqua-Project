using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;
    public int health;
    public int armour;
    public float speed;
    public Joystick joystick;

    private float startTime;
    private float direction;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();

        startTime = 0.0f;
    }

    public void TakeDamage(int amount)
    {
        int healthDamage = amount;
        health -= healthDamage * armour;

        if (health <= 0)
        {
            game.GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 stickMovement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            startTime = 0.0f;
        }

        //delay quarter second before starting movement to allow for tap and swipe
        if (Time.time > startTime + 0.25f && startTime != 0.0f)
        {
            //Joystick movement
            rigidbody.velocity = new Vector3(joystick.Horizontal * speed, rigidbody.velocity.y, joystick.Vertical * speed);

            transform.rotation = Quaternion.LookRotation(stickMovement);
            transform.Translate(stickMovement * speed * Time.deltaTime, Space.World);
        }
        else
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
        }

        //movement for PC
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
        }
    }
}
