using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameDialogue : MonoBehaviour
{
    [SerializeField] private CurrentLevel level;
    [SerializeField] private Image character;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Navigator navigator;
    [SerializeField] private GameObject dialogueParent;

    private DialogueLine[] _currentDialogue;
    private int _nextIndex = 0;
    private Action _onDialogueFinished;

    private void OnEnable() => Message.Subscribe<LevelCompleted>(e => End(), this);
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
            StartCoroutine(NavigateAfterDelay());
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
            character.sprite = _currentDialogue[_nextIndex].Character;
            text.text = _currentDialogue[_nextIndex].Text;
            _nextIndex++;
        }
    }

    private IEnumerator NavigateAfterDelay()
    {
        yield return new WaitForSeconds(1);
        navigator.NavigateToRewards();
    }
}

