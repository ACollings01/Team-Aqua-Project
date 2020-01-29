﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Staff : MonoBehaviour
{
    private Animator staffAnimator;
    Collider staffCollider;
    private bool quickTap = false;
    private bool longTap = false;
    private float startTime;

    bool AnimatorIsPlaying()
    {
        return staffAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject staff = transform.gameObject;
        staffAnimator = staff.GetComponent<Animator>();

        staffCollider = GetComponent<Collider>();

        startTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Tooch");
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
                    quickTap = false;
                }
                else if (Time.time < startTime + 1.0f && startTime != 0.0f)
                {
                    quickTap = true;
                }

                startTime = 0;
            }
        }

        if (quickTap && AnimatorIsPlaying())
        {
            staffAnimator.SetBool("Quick Tap Staff", true);
        }
        else if (!AnimatorIsPlaying())
        {
            staffAnimator.SetBool("Quick Tap Staff", false);
            quickTap = false;
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
}
