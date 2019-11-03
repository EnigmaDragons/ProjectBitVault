using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
            exitButton.enabled = false;
        playButton.onClick.AddListener(navigator.NavigateToLevelSelect);
        creditsButton.onClick.AddListener(navigator.NavigateToCredits);
        optionsButton.onClick.AddListener(() => Debug.Log("Options Button was Pressed"));
        exitButton.onClick.AddListener(Application.Quit);
    }
}
