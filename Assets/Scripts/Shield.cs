using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapons
{
    private Animator shieldAnimator;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip lightAttackSound;

    [SerializeField]
    private AudioClip heavyAttackSound;
    Rigidbody playerBody;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        shieldAnimator = Player.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

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

                if (attackOnce == false)
                {
                    audioSource.PlayOneShot(lightAttackSound);
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

                playerBody.velocity = transform.parent.forward * 40;

                if (attackOnce == false)
                {
                    audioSource.PlayOneShot(heavyAttackSound);
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

#if UNITY_ANDROID && !UNITY_EDITOR
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

        if (quickTap)
        {
            fireRate = 0.75f;

            if ((Time.time - lastFireTime) > fireRate)
            {
                lastFireTime = Time.time;
                shieldAnimator.SetTrigger("Quick Tap Shield");

                if (attackOnce == false)
                {
                    audioSource.PlayOneShot(lightAttackSound);
                    shieldAttack();
                }
            }

            quickTap = false;
        }

        if (longTap)
        {
            fireRate = 5f;

            if ((Time.time - lastFireTimeHeavy) > fireRate)
            {
                lastFireTimeHeavy = Time.time;

                shieldAnimator.SetTrigger("Long Tap Shield");

                if (Physics.Raycast(ray, out hit, 1000))
                {
                    lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
                }

                Player.transform.LookAt(lookAtClick);

                playerBody.velocity = transform.parent.forward * 40;

                if (attackOnce == false)
                {
                    audioSource.PlayOneShot(heavyAttackSound);
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

    }
}