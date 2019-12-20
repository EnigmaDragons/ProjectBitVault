using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewGameButtons : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private Toggle skipStory;
    [SerializeField] private Button male;
    [SerializeField] private Button female;
    [SerializeField] private Button start;
    [SerializeField] private BoolVariable autoSkip;
    [SerializeField] private BoolVariable useFemale;
    [SerializeField] private SaveStorage saveStorage;

    private bool _autoSkip = false;
    private bool _useFemale = false;

    private void Awake()
    {
        skipStory.onValueChanged.AddListener(SetValue);
        male.onClick.AddListener(SelectMale);
        female.onClick.AddListener(SelectFemale);
        start.onClick.AddListener(StartCommand);
        OnEnable();
    }

    private void OnEnable()
    {
        skipStory.isOn = false;
        _autoSkip = false;
        _useFemale = useFemale.Value;
        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        EventSystem.current.SetSelectedGameObject(_useFemale ? female.gameObject : male.gameObject);
    }

    private void SetValue(bool isActive)
    {
        _autoSkip = isActive;
        UpdateHighlight();
    }

    private void SelectMale()
    {
        _useFemale = false;
        UpdateHighlight();
    }

    private void SelectFemale()
    {
        _useFemale = true;
        UpdateHighlight();
    }

    private void StartCommand()
    {
        saveStorage.Reset();
        saveStorage.SetUseFemale(_useFemale);
        saveStorage.SetAutoSkipStory(_autoSkip);
        useFemale.Value = _useFemale;
        autoSkip.Value = _autoSkip;
        navigator.NavigateToLevelSelect();
    }
}
