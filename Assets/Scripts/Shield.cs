using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapons
{
    private Animator shieldAnimator;
    Rigidbody playerBody;

    bool AnimatorIsPlaying()
    {
        return shieldAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    void Start()
    {
        GameObject shield = transform.gameObject;
        shieldAnimator = shield.GetComponent<Animator>();
      
        startTime = 0.0f;

        Player = GameObject.FindGameObjectWithTag("Player");
        playerBody = Player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        lengthOfTap();

        if (quickTap && AnimatorIsPlaying())
        {
            shieldAnimator.SetBool("Quick Tap Shield", true);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
            }

            Player.transform.LookAt(lookAtClick);

            if (attackOnce == false)
            {
                shieldAttack();
            }
        }
        else if (!AnimatorIsPlaying())
        {
            shieldAnimator.SetBool("Quick Tap Shield", false);
            attackOnce = false;
            quickTap = false;
        }

        if (longTap && AnimatorIsPlaying())
        {
            shieldAnimator.SetBool("Long Tap Shield", true);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
            }

            Player.transform.LookAt(lookAtClick);

            playerBody.isKinematic = false;

            playerBody.velocity = transform.parent.forward * 10;

            if (attackOnce == false)
            {
                shieldAttack();
            }
        }
        else if (!AnimatorIsPlaying())
        {
            shieldAnimator.SetBool("Long Tap Shield", false);
            playerBody.isKinematic = true;
            attackOnce = false;
            longTap = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        GameObject playerShield = GameObject.Find("Player/Player_Model/Shield");
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerShield.transform.position, 1);
    }
}