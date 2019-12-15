using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Button alternateContinueButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private BoolVariable IsLevelStart;
    [SerializeField] private BoolReference OnlyStory;
    [SerializeField] private BoolReference developmentToolsActive;
    [SerializeField] private DialogueLine BetweenLevelDialogue;

    private DialogueLine[] _currentDialogue;
    private int _nextIndex = 0;
    private GameObject _customDisplayInstance;

    private void Awake()
    {
        continueButton.onClick.AddListener(Continue);
        alternateContinueButton.onClick.AddListener(Continue);
        skipButton.onClick.AddListener(Skip);
    }

    private void Start()
    {
        var dialogue = IsLevelStart.Value 
            ? developmentToolsActive && OnlyStory.Value 
                ? level.ActiveLevel.OpeningDialogue.Lines.Concat(new List<DialogueLine> { BetweenLevelDialogue }).ToArray() 
                : level.ActiveLevel.OpeningDialogue.Lines 
            : level.ActiveLevel.ClosingDialogue.Lines;
        if (dialogue.Length == 0)
            Finish();
        else
        {
            _currentDialogue = dialogue;
            _nextIndex = 0;
            Continue();
        }
    }

    private void Finish()
    {
        if (IsLevelStart.Value && OnlyStory.Value)
        {
            IsLevelStart.Value = false;
            navigator.NavigateToDialogue();
        }
        else if (IsLevelStart.Value) 
            navigator.NavigateToGameScene();
        else
            navigator.NavigateToRewards();
    }

    public void Continue()
    {
        if (_nextIndex == _currentDialogue.Length)
            Finish();
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

    public void Skip()
    {
        Finish();
    }
}

