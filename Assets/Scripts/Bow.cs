using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Bow : RangedWeapons
{
    private Animator bowAnimator;
    private bool quickTap = false;
    private bool longTap = false;
    private float startTime;
    private int damageBow;
    private GameObject Player;
    private GameObject Arrow;
    private float distanceToPlayer;

    Vector3 lookAtClick;
    Vector3 playerPosition;
    Vector3 arrowPosition;

    bool AnimatorIsPlaying()
    {
        return bowAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    void Start()
    {
        GameObject bow = transform.gameObject;

        Player = GameObject.FindGameObjectWithTag("Player");

        bowAnimator = bow.GetComponent<Animator>();

        startTime = 0.0f;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("Tooch");
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTime = Time.time;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (Time.time > startTime + 1.0f && startTime != 0.0f)
                {
                    quickTap = false;
                }
                else if (Time.time < startTime + 1.0f && startTime != 0.0f)
                {
                    quickTap = true;

                    if (Physics.Raycast(ray, out hit, 1000))
                    {
                        lookAtClick = hit.point;
                    }
                }

                startTime = 0;
            }
        }

        if (quickTap && AnimatorIsPlaying())
        {
            bowAnimator.SetBool("Quick Tap Bow", true);

            Player.transform.LookAt(lookAtClick);
            
        }
        else if (!AnimatorIsPlaying())
        {
            bowAnimator.SetBool("Quick Tap Bow", false);

            if (quickTap)
            {
                Player.transform.LookAt(lookAtClick);
                if ((Time.time - lastFireTime) > fireRate)
                {
                    lastFireTime = Time.time;

                    Fire();
                }
            }

            quickTap = false;
        }

        //for PC controls
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            startTime = 0.0f;
        }

        playerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        arrowPosition = new Vector3(arrow.transform.position.x, arrow.transform.position.y, arrow.transform.position.z);

        distanceToPlayer = Vector3.Distance(playerPosition, arrowPosition);

        if (distanceToPlayer > 30)
        {
            if (gameObject.tag == "Arrow")
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            bowDamageDone();
        }
    }

    public int bowDamageDone()
    {
        damageBow = Random.Range(4, 8);
        return damageBow;
    }
}
