using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button openHintMenu;
    [SerializeField] private Button addHintButton;
    [SerializeField] private Button clearHintButton;
    [SerializeField] private TextMeshProUGUI hintCount;
    [SerializeField] private SaveStorage saveStorage;
    [SerializeField] private CurrentLevel level;
    [SerializeField] private Toggle useHints;
    [SerializeField] private TextMeshProUGUI cost;

    private HintExecutor _hintExecutor;
    private int _cost;

    private void Start()
    {
        var hintsAreActive = level.ActiveLevel.Solution.IsPresent;
        useHints.SetIsOnWithoutNotify(saveStorage.GetUseHints());
        useHints.onValueChanged.AddListener(x => saveStorage.SetUseHints(x));
        openHintMenu.gameObject.SetActive(hintsAreActive);
        _hintExecutor = FindObjectOfType<HintExecutor>();
        hintCount.text = $"Current Level Hints: {saveStorage.GetHints(level.ActiveLevel)}";
        _cost = (int)Math.Pow(2, saveStorage.GetHints(level.ActiveLevel));
        cost.text = $"Cost: {_cost} H";
        addHintButton.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            _hintExecutor.AddHint();
            saveStorage.SetHintPoints(saveStorage.GetHintPoints() - _cost);
            hintCount.text = $"Current Level Hints: {saveStorage.GetHints(level.ActiveLevel)}";
            _cost = (int)Math.Pow(2, saveStorage.GetHints(level.ActiveLevel));
            cost.text = $"Cost: {_cost} H";
        });
        clearHintButton.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            saveStorage.ClearHints(level.ActiveLevel);
            hintCount.text = $"Current Level Hints: {saveStorage.GetHints(level.ActiveLevel)}";
            _cost = (int)Math.Pow(2, saveStorage.GetHints(level.ActiveLevel));
            cost.text = $"Cost: {_cost} H";
            Message.Publish(new LevelResetRequested());
        });
    }

    private void Update()
    {
        addHintButton.interactable = _cost <= saveStorage.GetHintPoints() && _hintExecutor.CanGiveHint;
    }
}
