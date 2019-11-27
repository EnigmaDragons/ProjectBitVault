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
    [SerializeField] private BoolReference IsLevelStart;

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
        var dialogue = IsLevelStart.Value ? level.ActiveLevel.OpeningDialogue : level.ActiveLevel.ClosingDialogue;
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
        if (IsLevelStart.Value)
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

