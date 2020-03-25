using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapons
{
    private Animator swordAnimator;
    
    /*bool AnimatorIsPlaying()
    {
        return swordAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }*/

    void Start()
    {
        GameObject sword = GameObject.Find("Player");
        swordAnimator = sword.GetComponent<Animator>();

        startTime = 0.0f;

        Player = GameObject.FindGameObjectWithTag("Player");
        layerMask = LayerMask.GetMask("Player", "Enemy");
        ignoreLayerMask = LayerMask.GetMask("Ignore Tap");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        layerMask = ~layerMask;

        lengthOfTap();

        if (Physics.Raycast(ray, out hit, 1000, ignoreLayerMask))
        {
            lookAtClick = lookAtClick;
        }
        else if (quickTap == false)
        {
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
            }
        }

        if (quickTap /*&& AnimatorIsPlaying()*/)
        {
            //swordAnimator.SetBool("Quick Tap Sword", true);
            swordAnimator.SetTrigger("Quick Tap Sword");

            //Player.transform.LookAt(lookAtClick);

            if (attackOnce == false)
            {
                swordAttack();
            }
            quickTap = false;
        }

        if (longTap)
        {
            swordAnimator.SetTrigger("Long Tap Sword");

            if (attackOnce == false)
            {
                swordAttack();
            }
            longTap = false;
        }

        if (!quickTap && !longTap)
        {
            attackOnce = false;
        }
        /*else if (!AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Quick Tap Sword", false);
            attackOnce = false;
            quickTap = false;
        }*/

        /*if (longTap && AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Long Tap Sword", true);

            if (attackOnce == false)
            {
                swordAttack();
            }
        }
        else if (!AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Long Tap Sword", false);
            attackOnce = false;
            longTap = false;
        }*/
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(SwordBlade.transform.position, 1);
    }
}
