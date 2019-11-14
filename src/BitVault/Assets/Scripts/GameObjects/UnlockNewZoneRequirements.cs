﻿using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockNewZoneRequirements : MonoBehaviour
{
    [SerializeField] private SaveStorage storage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameZones zones;
    [SerializeField] private GameObject locked;
    [SerializeField] private Button nextZoneButton;
    [SerializeField] private BoolVariable developmentToolsEnabled;

    private int _zone = 0;

    private void OnEnable()
    {
        UpdateRequirements();
    }

    private void Update()
    {
        if (_zone != storage.GetZone())
            UpdateRequirements();
    }

    private void UpdateRequirements()
    {
        _zone = storage.GetZone();

        if (zones.Value.Length == _zone + 1 || developmentToolsEnabled.Value)
        {
            text.text = "";
            locked.SetActive(false);
            nextZoneButton.interactable = true;
        }
        else if (storage.GetStars(zones.Value[_zone].Value.Last()) == 0)
        {
            text.text = "Complete All Levels";
            locked.SetActive(true);
            nextZoneButton.interactable = false;
        }
        else if (storage.GetTotalStars() < zones.Value[_zone + 1].StarsRequired)
        {
            text.text = $"{zones.Value[_zone + 1].StarsRequired} Stars Required";
            locked.SetActive(true);
            nextZoneButton.interactable = false;
        }
        else
        {
            text.text = "";
            locked.SetActive(false);
            nextZoneButton.interactable = true;
        }
    }
}