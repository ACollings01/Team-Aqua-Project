using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;
    public float health = 50f;
    float lastHP;
    public int armour;
    public float speed;
    public Joystick joystick;
    protected bool characterMoving = false;
    protected bool checkJoystickOnce = false;
    public bool stopMoving = false;
    private Animator playerAnimator;
    private AudioSource playerAudioSource;
    private AudioSource deathAudioSource;


    public ParticleSystem blood;

    // Start is called before the first frame update
    void Start()
    {
        lastHP = health; //So blood doesn't randomly come out of the player on Spawn

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject joystickObject = GameObject.Find("Fixed Joystick");

        playerAnimator = player.GetComponent<Animator>();

        playerAudioSource = GetComponent<AudioSource>();

#if UNITY_EDITOR
        if (joystickObject != null)
        {
            joystickObject.SetActive(false);
        }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        if (joystickObject != null)
        {
            joystickObject.SetActive(true);
            joystick = FindObjectOfType<Joystick>();
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        //Debug.Log(stopMoving);
        if (!stopMoving)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //movement for PC
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && movement != Vector3.zero)
            {
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && !characterMoving)
                {
                    playerAudioSource.Play();
                }
                transform.rotation = Quaternion.LookRotation(movement);
                transform.Translate(movement * speed * Time.deltaTime, Space.World);
                characterMoving = true;
                playerAnimator.SetBool("IsMoving", true);
            }
            else if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)))
            {
                characterMoving = false;
                playerAnimator.SetBool("IsMoving", false);
                playerAudioSource.Stop();
            }
        }
        else if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)))
        {
            characterMoving = false;
            playerAnimator.SetBool("IsMoving", false);
            playerAudioSource.Stop();
        }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        Vector3 stickMovement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        var rigidbody = GetComponent<Rigidbody>();
        if (!stopMoving)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (Input.touchCount < 2 && touch.phase == TouchPhase.Ended)
                    {
                        characterMoving = false;
                        playerAnimator.SetBool("IsMoving", false);
                        playerAudioSource.Stop();
                    }

                    if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            characterMoving = true;
                            playerAnimator.SetBool("IsMoving", true);
                            playerAudioSource.Play();
                        }

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
                }
            }
        }
        
#endif
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
        if(health < lastHP)
        {
            lastHP = health;
            var bloodSystem = Instantiate(blood, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
            Destroy(bloodSystem.gameObject, 1f);
        }

        if (health <= 0)
        {
            Destroy(this.gameObject, 5.0f);
        }
    }

    public void TakeDamage(int amount)
    {
        int healthDamage = amount;
        health -= healthDamage * armour;

        if (health <= 0)
        {
            game.GameOver();
            deathAudioSource = GetComponent<AudioSource>();
        }
        
    }
}
