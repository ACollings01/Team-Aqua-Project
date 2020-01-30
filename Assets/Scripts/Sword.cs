using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator swordAnimator;
    Collider swordCollider;
    private bool quickTap = false;
    private bool longTap = false;
    private float startTime;
    private int damage;


    bool AnimatorIsPlaying()
    {
        return swordAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject sword = transform.gameObject;
        swordAnimator = sword.GetComponent<Animator>();

        swordCollider = GetComponent<Collider>();

        startTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
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
                    longTap = true;
                    quickTap = false;
                }
                else if (Time.time < startTime + 1.0f && startTime != 0.0f)
                {
                    quickTap = true;
                    longTap = false;
                }

                startTime = 0;
            }
        }

        if (quickTap && AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Quick Tap", true);
            swordCollider.enabled = true;
        }
        else if (!AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Quick Tap", false);
            swordCollider.enabled = false;
            quickTap = false;
        }

        if (longTap && AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Long Tap", true);
            swordCollider.enabled = true;
        }
        else if (!AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Long Tap", false);
            swordCollider.enabled = false;
            longTap = false;
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
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy.gameObject.GetComponent<EnemyAI>().health -= swordDamageDone();
        }
    }

    public int swordDamageDone()
    {
        damage = Random.Range(8, 11);
        return damage;
    }
}
