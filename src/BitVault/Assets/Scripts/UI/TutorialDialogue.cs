using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] private CurrentTutorial level;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Navigator navigator;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button returnButton;

    private int _index = 0;
    private GameObject _customDisplayInstance;

    private void Awake()
    {
        nextButton.onClick.AddListener(Next);
        previousButton.onClick.AddListener(Previous);
        returnButton.onClick.AddListener(Return);
    }

    private void Start()
    {
        _index = 0;
        UpdateTutorial();
    }

    public void Next()
    {
        _index++;
        _index = Mathf.Min(_index, level.Tutorial.Lines.Length);
        UpdateTutorial();
    }

    public void Previous()
    {
        _index--;
        _index = Mathf.Max(_index, 0);
        UpdateTutorial();
    }

    private void Return()
    {
        navigator.NavigateBack();
    }

    private void UpdateTutorial()
    {
        var line = level.Tutorial.Lines[_index];
        text.text = Regex.Unescape(line.Text);
        previousButton.gameObject.SetActive(_index != 0);
        nextButton.gameObject.SetActive(_index != level.Tutorial.Lines.Length - 1);
        if (_customDisplayInstance)
            Destroy(_customDisplayInstance);
        if (line.CustomDisplay.IsPresent)
            _customDisplayInstance = Instantiate(line.CustomDisplay.Value);
    }
}
