﻿using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private Button playButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject exitButtonObject;
    [SerializeField] private CurrentTutorial tutorial;
    [SerializeField] private Dialogue masterTutorial;

    private void Awake()
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
            exitButtonObject.SetActive(false);
        playButton.onClick.AddListener(navigator.NavigateToLevelSelect);
        tutorialButton.onClick.AddListener(() =>
        {
            tutorial.Set(masterTutorial);
            navigator.NavigateToTutorial();
        });
        exitButton.onClick.AddListener(Application.Quit);
    }
}
