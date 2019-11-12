using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MultiGridLayoutGroup : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject gridLayoutGroup;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject previousPageButton;
    [SerializeField] private TextMeshProUGUI pageNumText;
    [SerializeField] private GameObject nextPageButton;

    private List<GameObject> _groups;
    private int _groupIndex;

    public void Init(GameObject elementTemplate, List<Action<GameObject>> initElement, int elementsPerGroup)
    {
        var list = new List<List<Action<GameObject>>>();
        for (var i = 0; i < initElement.Count; i += elementsPerGroup)
            list.Add(initElement.Skip(i).Take(elementsPerGroup).ToList());
        Init(elementTemplate, list);
    }

    public void Init(GameObject elementTemplate, List<List<Action<GameObject>>> initElementGroups)
    {
        _groups?.ForEach(Destroy);
        _groups = new List<GameObject>();
        initElementGroups.ForEach(x => AddGroup(elementTemplate, x));
        _groupIndex = 0;
        _groups[_groupIndex].SetActive(true);
        UpdatePageControls();
    }

    private void AddGroup(GameObject elementTemplate, List<Action<GameObject>> initElement)
    {
        var group = Instantiate(gridLayoutGroup, parent);
        initElement.ForEach(x =>
        {
            var element = Instantiate(elementTemplate, group.transform);
            x(element);
        });
        group.SetActive(false);
        _groups.Add(group);
    }

    public void PreviousPage()
    {
        _groups[_groupIndex].SetActive(false);
        _groupIndex--;
        _groups[_groupIndex].SetActive(true);
        UpdatePageControls();
    }

    public void NextPage()
    {
        _groups[_groupIndex].SetActive(false);
        _groupIndex++;
        _groups[_groupIndex].SetActive(true);
        UpdatePageControls();
    }

    private void UpdatePageControls()
    {
        controls.SetActive(_groups.Count != 1);
        previousPageButton.SetActive(_groupIndex != 0);
        nextPageButton.SetActive(_groupIndex != _groups.Count - 1);
        pageNumText.text = (_groupIndex + 1).ToString();
    }
}
