using UnityEngine;

public sealed class ToggleTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;

    public void Toggle() => target.SetActive(!target.activeSelf);
}

