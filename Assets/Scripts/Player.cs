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
    bool characterMoving = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        joystick = FindObjectOfType<Joystick>();
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
        var flat = GetComponent<Transform>();

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 stickMovement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (Input.GetMouseButtonDown(0))
        {
            characterMoving = true;
            SoundManager.Instance.PlayClip(audioSource);
        }

        if (Input.GetMouseButtonUp(0))
        {
            characterMoving = false;
        }

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (characterMoving)
            {
                //Joystick movement
                rigidbody.velocity = new Vector3(joystick.Horizontal * speed, rigidbody.velocity.y, joystick.Vertical * speed);

                transform.rotation = Quaternion.LookRotation(stickMovement);
                transform.Translate(stickMovement * speed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            if (characterMoving)
            {
                //Joystick movement
                rigidbody.velocity = new Vector3(joystick.Horizontal * speed, rigidbody.velocity.y, joystick.Vertical * speed);

                if (stickMovement != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(stickMovement);
                    transform.Translate(stickMovement * speed * Time.deltaTime, Space.World);
                }               
            }
        }

        //movement for PC
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

        CheckHealth();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Debug.Log("Player Died");
        }
    }
}
