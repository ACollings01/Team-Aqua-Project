using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapons
{
    private Animator shieldAnimator;
    Rigidbody playerBody;
    private AudioSource audioSource;

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
        layerMask = LayerMask.GetMask("Player", "Enemy");
        ignoreLayerMask = LayerMask.GetMask("Ignore Tap");

        playerBody = Player.GetComponent<Rigidbody>();
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

        if (quickTap && AnimatorIsPlaying())
        {
            shieldAnimator.SetBool("Quick Tap Shield", true);

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

        SoundManager.Instance.PlayClip(audioSource);

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