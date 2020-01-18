using System;
using TMPro;
using UnityEngine;

public class TimePlayedHintBonus : MonoBehaviour
{
    [SerializeField] private SaveStorage storage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int minutesPerBonus;
    [SerializeField] private int maxBonusPerLevel;

    private float _time;
    private int _bonusesGiven;
    private bool _givingBonus;

    private void Start()
    {
        _time = 0;
        _bonusesGiven = 0;
        _givingBonus = false;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > 60)
        {
            _time -= 60;
            storage.SetMinutesTilNextHintBonus(Math.Max(0, storage.MinutesTilNextHintBonus() - 1));
            if (storage.MinutesTilNextHintBonus() == 0 && _bonusesGiven != maxBonusPerLevel)
            {
                _givingBonus = true;
                storage.SetMinutesTilNextHintBonus(minutesPerBonus);
            }
        }

        if (_givingBonus)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Math.Min(1, text.color.a + Time.deltaTime));
            if (text.color.a == 1)
            {
                _bonusesGiven++;
                storage.SetHintPoints(storage.GetHintPoints() + 1);
                _givingBonus = false;
            }
        }
        else
            text.color = new Color(text.color.r, text.color.g, text.color.b, Math.Max(0, text.color.a - Time.deltaTime));
    }
}
