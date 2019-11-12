using UnityEngine;

public class DisableGameInputWhileActive : MonoBehaviour
{
    [SerializeField] private BoolVariable gameInputActive;

    private void OnEnable() => gameInputActive.Value = false;
    private void OnDisable() => gameInputActive.Value = true;
}
