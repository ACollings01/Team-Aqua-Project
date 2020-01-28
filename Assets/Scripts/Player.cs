using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;
    public float health;
    public float armour;
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

    public void TakeDamage(float amount)
    {
        float healthDamage = amount;
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

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 stickMovement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            startTime = 0.0f;
            // Debug.Log("startTime" + startTime);
        }

        if (Time.time > startTime + 0.25f && startTime != 0.0f)
        {
            //Debug.Log("startTime" + startTime);
            rigidbody.velocity = new Vector3(joystick.Horizontal * speed, rigidbody.velocity.y, joystick.Vertical * speed);

            transform.rotation = Quaternion.LookRotation(stickMovement);
            transform.Translate(stickMovement * speed * Time.deltaTime, Space.World);
        }
        else
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
        }

        transform.Translate(movement * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
        }
    }
}
