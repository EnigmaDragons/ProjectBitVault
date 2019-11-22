using UnityEngine;
using UnityEngine.UI;

public class ToggleGameObject : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Button enableButton;
    [SerializeField] private Button disableButton;

    void OnEnable()
    {
        enableButton.onClick.AddListener(EnableTarget);
        disableButton.onClick.AddListener(DisableTarget);
    }

    private void OnDisable()
    {
        enableButton.onClick.RemoveListener(EnableTarget);
        disableButton.onClick.RemoveListener(DisableTarget);
    }

    private void DisableTarget() => target.SetActive(false);
    private void EnableTarget() => target.SetActive(true);
}
