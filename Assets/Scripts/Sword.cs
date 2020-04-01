using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapons
{
    private Animator swordAnimator;
    private AudioSource audioSource;

    void Start()
    {
#if UNITY_EDITOR
        GameObject sword = GameObject.Find("Player");
        swordAnimator = sword.GetComponent<Animator>();
        startTime = 0.0f;

        layerMask = LayerMask.GetMask("Player", "Enemy");
        ignoreLayerMask = LayerMask.GetMask("Ignore Tap");

        audioSource = GetComponent<AudioSource>();
#else
        GameObject sword = GameObject.Find("Player");
        swordAnimator = sword.GetComponent<Animator>();
        startTime = 0.0f;

        layerMask = LayerMask.GetMask("Player", "Enemy");
        ignoreLayerMask = LayerMask.GetMask("Ignore Tap");

        audioSource = GetComponent<AudioSource>();
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("space"))
        {
            swordAnimator.SetTrigger("Quick Tap Sword");

            audioSource.Play();

            if (attackOnce == false)
            {
                swordAttack();
            }
            quickTap = false;
        }

        if (Input.GetKeyDown("v"))
        {
            swordAnimator.SetTrigger("Long Tap Sword");

            audioSource.Play();

            if (attackOnce == false)
            {
                swordAttack();
            }
            quickTap = false;
        }

        if (!quickTap && !longTap)
        {
            attackOnce = false;
        }
#else
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        layerMask = ~layerMask;

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

        lengthOfTap();

        if (quickTap)
        {
            swordAnimator.SetTrigger("Quick Tap Sword");

            audioSource.Play();

            Player.transform.LookAt(lookAtClick);

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
#endif 
    }
}
