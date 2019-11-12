using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MultiGridLayoutGroup : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject gridLayoutGroup;
    [SerializeField] private IntReference elementsPerGroup;
    [SerializeField] private bool shouldHaveAtLeastOneDefault;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject previousPageButton;
    [SerializeField] private TextMeshProUGUI pageNumText;
    [SerializeField] private GameObject nextPageButton;

    private List<GameObject> _groups;
    private int _groupIndex;

    public void Init(GameObject elementTemplate, List<Action<GameObject>> initElement) => Init(elementTemplate, initElement, new GameObject("default"), x => { });
    public void Init(GameObject elementTemplate, List<Action<GameObject>> initElement, GameObject defaultElementTemplate, Action<GameObject> initDefaultElement)
    {
        _groups?.ForEach(Destroy);
        _groups = new List<GameObject>();
        for (var i = 0; i < (initElement.Count + (shouldHaveAtLeastOneDefault || initElement.Count == 0 ? 1 : 0)); i += elementsPerGroup.Value)
            AddGroup(elementTemplate, defaultElementTemplate, initElement.Skip(i).Take(elementsPerGroup.Value).ToList(), initDefaultElement);
        _groupIndex = 0;
        _groups[_groupIndex].SetActive(true);
        UpdatePageControls();
    }

    private void AddGroup(GameObject elementTemplate, GameObject defaultElementTemplate, List<Action<GameObject>> initElement, Action<GameObject> initDefaultElement)
    {
        var group = Instantiate(gridLayoutGroup, parent);
        for (var i = 0; i < elementsPerGroup.Value; i++)
        {
            var element = Instantiate(i < initElement.Count ? elementTemplate : defaultElementTemplate, group.transform);
            if (i < initElement.Count)
                initElement[i](element);
            else
                initDefaultElement(element);
        }
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
