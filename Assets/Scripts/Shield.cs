using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapons
{
    private Animator shieldAnimator;
    Rigidbody playerBody;
    private AudioSource audioSource;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        shieldAnimator = Player.GetComponent<Animator>();
      
        startTime = 0.0f;

        playerBody = Player.GetComponent<Rigidbody>();

        lastFireTime = Time.time - 10;
        lastFireTimeHeavy = Time.time - 10;

#if UNITY_ANDROID && !UNITY_EDITOR
        layerMask = LayerMask.GetMask("Player", "Enemy");
        ignoreLayerMask = LayerMask.GetMask("Ignore Tap");
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("space"))
        {
            fireRate = 0.75f;

            if ((Time.time - lastFireTime) > fireRate)
            {
                lastFireTime = Time.time;
                shieldAnimator.SetTrigger("Quick Tap Shield");

                //audioSource.Play();

                if (attackOnce == false)
                {
                    shieldAttack();
                }
            }

            quickTap = false;
        }

        if (Input.GetKeyDown("v"))
        {
            fireRate = 5f;

            if ((Time.time - lastFireTimeHeavy) > fireRate)
            {
                lastFireTimeHeavy = Time.time;

                shieldAnimator.SetTrigger("Long Tap Shield");

                //audioSource.Play();

                playerBody.velocity = transform.parent.forward * 40;

                if (attackOnce == false)
                {
                    shieldAttack();
                }
            }

            longTap = false;
        }

        if (!quickTap && !longTap)
        {
            attackOnce = false;
        }
#endif
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    layerMask = ~layerMask;

        //    lengthOfTap();

        //    if (Physics.Raycast(ray, out hit, 1000, ignoreLayerMask))
        //    {
        //        lookAtClick = lookAtClick;
        //    }
        //    else if (quickTap == false)
        //    {
        //        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        //        {
        //            lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
        //        }
        //    }

        //    if (quickTap && AnimatorIsPlaying())
        //    {
        //        shieldAnimator.SetBool("Quick Tap Shield", true);

        //        Player.transform.LookAt(lookAtClick);

        //        if (attackOnce == false)
        //        {
        //            shieldAttack();
        //        }
        //    }
        //    else if (!AnimatorIsPlaying())
        //    {
        //        shieldAnimator.SetBool("Quick Tap Shield", false);
        //        attackOnce = false;
        //        quickTap = false;
        //    }

        //    SoundManager.Instance.PlayClip(audioSource);

        //    if (longTap && AnimatorIsPlaying())
        //    {
        //        shieldAnimator.SetBool("Long Tap Shield", true);

        //        if (Physics.Raycast(ray, out hit, 1000))
        //        {
        //            lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
        //        }

        //        Player.transform.LookAt(lookAtClick);

        //        playerBody.isKinematic = false;

        //        playerBody.velocity = transform.parent.forward * 10;

        //        if (attackOnce == false)
        //        {
        //            shieldAttack();
        //        }
        //    }
        //    else if (!AnimatorIsPlaying())
        //    {
        //        shieldAnimator.SetBool("Long Tap Shield", false);
        //        playerBody.isKinematic = true;
        //        attackOnce = false;
        //        longTap = false;
        //    }
    }

    void OnDrawGizmosSelected()
    {
        GameObject playerShield = GameObject.Find("Player_Shield/Shield/Shield Body");
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerShield.transform.position, 1);
    }
}