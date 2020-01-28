using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    private Animator swordAnimator;
    Collider swordCollider;
    private float startTime;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        GameObject sword = transform.gameObject;
        swordAnimator = sword.GetComponent<Animator>();

        swordCollider = GetComponent<Collider>();
        swordCollider.isTrigger = false;

        startTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //for joystick controls
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            startTime = 0.0f;
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

        if (Time.time < startTime + 0.24f && startTime != 0.0f)
        {
            swordAnimator.SetBool("Quick Click", true);
            swordCollider.isTrigger = true;
        }
        else
        {
            swordAnimator.SetBool("Quick Click", false);
            swordCollider.isTrigger = false;
        }
    }

    void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Bandit")
        {
            swordDamageDone();
        }
    }

    public int swordDamageDone()
    {
        damage = Random.Range(8, 11);
        return damage;
    }
}
