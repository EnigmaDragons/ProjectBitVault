using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditStrings : MonoBehaviour
{
    [SerializeField] private List<string> Strings;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private GameObject[] ExtraneousObjects;
    private int _index;

    private void Start()
    {
        _index = 0;
        Text.text = Strings[_index];
    }

    public void Next()
    {
        _index++;
        Text.text = _index >= Strings.Count ? "" : Strings[_index];
        if (_index == Strings.Count)
            ExtraneousObjects.ForEach(x => x.SetActive(false));
    }
}
