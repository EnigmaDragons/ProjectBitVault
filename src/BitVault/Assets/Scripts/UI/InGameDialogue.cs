using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameDialogue : MonoBehaviour
{
    [SerializeField] private CurrentLevel level;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image bust;
    [SerializeField] private Navigator navigator;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject dialogueParent;

    private DialogueLine[] _currentDialogue;
    private int _nextIndex = 0;
    private Action _onDialogueFinished;
    private GameObject _customDisplayInstance;

    private void Awake() => continueButton.onClick.AddListener(Continue);
    private void OnEnable() => Message.Subscribe<EndingLevelAnimationFinished>(e => End(), this);
    private void OnDisable() => Message.Unsubscribe(this);

    private void Start()
    {
        var openingDialogue = level.ActiveLevel.OpeningDialogue;
        if (openingDialogue.Length == 0)
            dialogueParent.SetActive(false);
        else
        {
            _currentDialogue = openingDialogue;
            _nextIndex = 0;
            _onDialogueFinished = () => dialogueParent.SetActive(false);
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
            dialogueParent.SetActive(true);
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
            if (_customDisplayInstance)
                Destroy(_customDisplayInstance);
            var line = _currentDialogue[_nextIndex];
            text.text = line.Text;
            name.text = line.Character.Name;
            bust.sprite = line.Character.Bust;
            if (line.CustomDisplay.IsPresent)
            {
                bust.gameObject.SetActive(false);
                _customDisplayInstance = Instantiate(line.CustomDisplay.Value, bust.transform.parent);
            }
            else
                bust.gameObject.SetActive(true);
            _nextIndex++;
        }
    }
}

