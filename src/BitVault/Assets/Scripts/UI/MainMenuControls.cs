using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject exitButtonObject;

    private void Awake()
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
            exitButtonObject.SetActive(false);
        playButton.onClick.AddListener(navigator.NavigateToLevelSelect);
        creditsButton.onClick.AddListener(navigator.NavigateToCredits);
        exitButton.onClick.AddListener(Application.Quit);
    }
}
