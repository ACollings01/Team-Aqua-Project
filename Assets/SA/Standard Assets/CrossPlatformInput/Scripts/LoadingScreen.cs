﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//reference from https://www.windykeep.com/2018/02/15/make-loading-screen-unity/

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;

    //private AsyncOperation currentLoadingOperation;
    private float timeElapsed;
    private bool isLoading;

    [SerializeField]
    private RectTransform barFillRectTransform;

    private Vector3 barFillLocalScale;

    [SerializeField]
    private Text percentLoadedText;
    private const float MIN_TIME_TO_SHOW = 10f;

    [SerializeField]
    private bool hideProgressBar;
    [SerializeField]
    private bool hidePercentageText;
    private Animator animator;
    private bool didTriggerFadeOutAnimation;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Configure();

        Show();
    }

    private void Configure()
    {
        barFillLocalScale = barFillRectTransform.localScale;
        barFillRectTransform.transform.parent.gameObject.SetActive(!hideProgressBar);
        // Enable/disable the percentage text based on configuration:
        percentLoadedText.gameObject.SetActive(!hidePercentageText);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isLoading)
        {
            timeElapsed += Time.deltaTime;
            SetProgress(timeElapsed / MIN_TIME_TO_SHOW);
            if (timeElapsed >= MIN_TIME_TO_SHOW)
            {
                animator.SetTrigger("Hide");
                didTriggerFadeOutAnimation = true;
                Hide();
            }
        }
    }

    private void SetProgress(float progress)
    {
        // Update the fill's scale based on how far the game has loaded:
        barFillLocalScale.x = progress;
        // Update the rect transform:
        barFillRectTransform.localScale = barFillLocalScale;
        // Set the percent loaded text:
        percentLoadedText.text = Mathf.CeilToInt(progress * 100).ToString() + "%";
    }

    public void Show()
    {
        // Enable the loading screen:
        gameObject.SetActive(true);
           // Reset the UI:
        SetProgress(0f);
        // Reset the time elapsed:
        timeElapsed = 0f;
        animator.SetTrigger("Show");
        didTriggerFadeOutAnimation = false;
        isLoading = true;
    }

    // Call this to hide it:
    public void Hide()
    {
        // Disable the loading screen:
        gameObject.SetActive(false);
        isLoading = false;
    }
}