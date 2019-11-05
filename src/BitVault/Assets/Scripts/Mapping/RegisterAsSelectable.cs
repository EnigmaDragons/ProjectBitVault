using UnityEngine;

public class RegisterAsSelectable : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap map;

    private void Start() => map.RegisterAsSelectable(gameObject);
}
  
