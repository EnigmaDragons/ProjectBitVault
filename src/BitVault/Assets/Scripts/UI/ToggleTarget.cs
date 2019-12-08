using UnityEngine;

public sealed class ToggleTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject[] additionalTargets;

    public void Toggle() => target.Concat(additionalTargets).ForEach(t => t.SetActive(!t.activeSelf));
}

