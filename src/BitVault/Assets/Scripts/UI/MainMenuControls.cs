using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject exitButtonObject;
    [SerializeField] private SaveStorage saveStorage;

    private void Awake()
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
            exitButtonObject.SetActive(false);
        continueButton.interactable = saveStorage.HasChosenGender();
        continueButton.onClick.AddListener(() => navigator.NavigateToLevelSelect());
        creditsButton.onClick.AddListener(() => navigator.NavigateToCredits());
        exitButton.onClick.AddListener(Application.Quit);
    }
}
