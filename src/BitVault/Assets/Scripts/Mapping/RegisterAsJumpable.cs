using UnityEngine;

public class RegisterAsJumpable : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap map;

    void Start() => map.RegisterAsJumpable(gameObject);
}
