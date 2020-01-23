using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelZonePagination : MonoBehaviour
{
    [SerializeField] private LevelZoneButtons buttons;
    [SerializeField] private GameObject controls;
    [SerializeField] private Button previousPageButton;
    [SerializeField] private TextMeshProUGUI pageNumText;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Campaign zones;
    [SerializeField] private SaveStorage storage;
    [SerializeField] private CurrentZone zone;
    [SerializeField] private TutorialButton tutorialButton;
    [SerializeField] private GoToStoryButton storyButton;

    private int ZoneCount => zones.Value.Length;
    private int _zoneIndex;

    public void Awake()
    {
        Init(storage.GetZone());
        nextPageButton.GetComponent<Button>().onClick.AddListener(NextPage);
        previousPageButton.GetComponent<Button>().onClick.AddListener(PreviousPage);
    }

    public void PreviousPage() => Change(_zoneIndex - 1);
    public void NextPage() => Change(_zoneIndex + 1);
    private void Init(int page) => Change(page);
    
    private void Change(int newIndex)
    {
        _zoneIndex = Math.Min(Math.Max(newIndex, 0), zones.Value.Length - 1);
        zone.Init(_zoneIndex);
        Render();
    }

    private void Render()
    {
        buttons.Init(_zoneIndex, zones.Value[_zoneIndex]);
        tutorialButton.Init(_zoneIndex);
        storyButton.Init(zones.Value[_zoneIndex]);
        controls.SetActive(ZoneCount > 1);
        previousPageButton.interactable = _zoneIndex != 0;
        nextPageButton.interactable = _zoneIndex != ZoneCount - 1;
        if (pageNumText != null)
            pageNumText.text = (_zoneIndex + 1).ToString();
    }
}
