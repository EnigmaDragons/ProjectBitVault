using UnityEngine;

public class SetActiveBasedOnBool : MonoBehaviour
{
    [SerializeField] private BoolReference boolRef;

    private void Start() => gameObject.SetActive(boolRef.Value);
}
