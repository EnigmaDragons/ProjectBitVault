using UnityEngine;
using UnityEngine.Events;

public sealed class OnKeyPress : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private UnityEvent action;

    private void Update()
    {
        if (Input.GetKeyDown(key))
            action.Invoke();
    }
}
