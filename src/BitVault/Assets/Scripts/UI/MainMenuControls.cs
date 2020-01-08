using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button storyArchiveButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject exitButtonObject;
    [SerializeField] private CurrentTutorial tutorial;
    [SerializeField] private Dialogue masterTutorial;
    [SerializeField] private SaveStorage saveStorage;

    private void Awake()
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
            exitButtonObject.SetActive(false);
        continueButton.interactable = saveStorage.HasChosenGender();
        continueButton.onClick.AddListener(() => navigator.NavigateToLevelSelect());
        storyArchiveButton.onClick.AddListener(() => navigator.NavigateToArchive());
        tutorialButton.onClick.AddListener(() =>
        {
            tutorial.Set(masterTutorial);
            navigator.NavigateToTutorial();
        });
        creditsButton.onClick.AddListener(() => navigator.NavigateToCredits());
        exitButton.onClick.AddListener(Application.Quit);
    }
}
