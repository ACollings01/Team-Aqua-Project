using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : RangedWeapons
{
    private Animator spearAnimator;

    GameObject[] spearProjectiles;

    bool AnimatorIsPlaying()
    {
        return spearAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    void Start()
    {
        GameObject spear = transform.gameObject;
        spearAnimator = spear.GetComponent<Animator>();

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
            spearAnimator.SetBool("Quick Tap Spear", true);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
            }

            Player.transform.LookAt(lookAtClick);

            if (attackOnce == false)
            {
                spearAttack();
            }
        }
        else if (!AnimatorIsPlaying())
        {
            spearAnimator.SetBool("Quick Tap Spear", false);
            attackOnce = false;
            quickTap = false;
        }

        if (longTap && AnimatorIsPlaying())
        {
            spearAnimator.SetBool("Long Tap Spear", true);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
            }

            Player.transform.LookAt(lookAtClick);

        }
        else if (!AnimatorIsPlaying())
        {
            spearAnimator.SetBool("Long Tap Spear", false);

            if (longTap)
            {
                Player.transform.LookAt(lookAtClick);

                if ((Time.time - lastFireTime) > fireRate)
                {
                    lastFireTime = Time.time;

                    ThrowSpear();
                }
            }

            longTap = false;
        }

        spearProjectiles = GameObject.FindGameObjectsWithTag("Thrown Spear");

        foreach (GameObject spearProjectile in spearProjectiles)
        {
            float distance = Vector3.Distance(spearProjectile.transform.position, Player.transform.position);

            if (distance > 50)
            {
                Destroy(spearProjectile);
            }

            /*if (Time.time > startTime + 5.0f && startTime != 0.0f)
            {
                Destroy(spearProjectile);
            }*/
        }

    }

    void OnDrawGizmosSelected()
    {
        GameObject playerSpear = GameObject.Find("Player/Player_Model/Spear/Spear_Tip");
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerSpear.transform.position, .5f);
    }
}