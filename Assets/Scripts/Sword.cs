using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapons
{
    private Animator swordAnimator;

    bool AnimatorIsPlaying()
    {
        return swordAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    void Start()
    {
        GameObject sword = transform.gameObject;
        swordAnimator = sword.GetComponent<Animator>();

        //swordCollider = GetComponent<Collider>();

        startTime = 0.0f;
    }

    void Update()
    {
        lengthOfTap();

        if (quickTap && AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Quick Tap", true);

            if (attackOnce == false)
            {
                swordAttack();
            }
        }
        else if (!AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Quick Tap", false);
            attackOnce = false;
            quickTap = false;
        }

        if (longTap && AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Long Tap", true);

            if (attackOnce == false)
            {
                swordAttack();
            }
        }
        else if (!AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Long Tap", false);
            attackOnce = false;
            longTap = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        GameObject playerSword = GameObject.Find("Player/Player_Model/Sword");
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerSword.transform.position, 1);
    }
}
