using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private Button playButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button storyArchiveButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject exitButtonObject;
    [SerializeField] private CurrentTutorial tutorial;
    [SerializeField] private Dialogue masterTutorial;

    private void Awake()
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
            exitButtonObject.SetActive(false);
        storyArchiveButton.onClick.AddListener(() => navigator.NavigateToArchive());
        tutorialButton.onClick.AddListener(() =>
        {
            tutorial.Set(masterTutorial);
            navigator.NavigateToTutorial();
        });
        exitButton.onClick.AddListener(Application.Quit);
    }
}
