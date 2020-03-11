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
        GameObject sword = GameObject.Find("Sword");
        swordAnimator = sword.GetComponent<Animator>();

        startTime = 0.0f;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        lengthOfTap();

        if (quickTap && AnimatorIsPlaying())
        {
            swordAnimator.SetBool("Quick Tap", true);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
            }

            Player.transform.LookAt(lookAtClick);

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(SwordBlade.transform.position, 1);
    }
}
