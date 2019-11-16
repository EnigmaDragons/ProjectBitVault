using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameDialogue : MonoBehaviour
{
    [SerializeField] private CurrentLevel level;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Navigator navigator;
    [SerializeField] private GameObject canvasDialogue;
    [SerializeField] private GameObject nonCanvasDialogue;
    [SerializeField] private Button continueButton;

    private GameObject _activeCanvasDisplay;
    private GameObject _activeNonCanvasDisplay;
    private DialogueLine[] _currentDialogue;
    private int _nextIndex = 0;
    private Action _onDialogueFinished;

    private void Awake() => continueButton.onClick.AddListener(Continue);
    private void OnEnable() => Message.Subscribe<EndingLevelAnimationFinished>(e => End(), this);
    private void OnDisable() => Message.Unsubscribe(this);

    private void Start()
    {
        var openingDialogue = level.ActiveLevel.OpeningDialogue;
        if (openingDialogue.Length == 0)
            SetDialogueActive(false);
        else
        {
            _currentDialogue = openingDialogue;
            _nextIndex = 0;
            _onDialogueFinished = () => SetDialogueActive(false);
            Continue();
        }
    }

    private void End()
    {
        var closingDialogue = level.ActiveLevel.ClosingDialogue;
        if (closingDialogue.Length == 0)
            navigator.NavigateToRewards();
        else
        {
            SetDialogueActive(true);
            _currentDialogue = closingDialogue;
            _nextIndex = 0;
            _onDialogueFinished = () => navigator.NavigateToRewards();
            Continue();
        }
    }

    public void Continue()
    {
        if (_nextIndex == _currentDialogue.Length)
            _onDialogueFinished();
        else
        {
            CleanUpCurrentDisplay();
            var line = _currentDialogue[_nextIndex];
            line.CanvasDisplay.IfPresent(x => _activeCanvasDisplay = Instantiate(x, canvasDialogue.transform));
            line.NonCanvasDisplay.IfPresent(x => _activeNonCanvasDisplay = Instantiate(x, nonCanvasDialogue.transform));
            text.text = line.Text;
            _nextIndex++;
        }
    }

    private void SetDialogueActive(bool isActive)
    {
        canvasDialogue.SetActive(isActive);
        nonCanvasDialogue.SetActive(isActive);
    }

    private void CleanUpCurrentDisplay()
    {
        if (_activeCanvasDisplay != null)
            Destroy(_activeCanvasDisplay);
        if (_activeNonCanvasDisplay != null)
            Destroy(_activeNonCanvasDisplay);
    }
}

