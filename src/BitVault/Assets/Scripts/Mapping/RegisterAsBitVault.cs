using UnityEngine;

public class RegisterAsBitVault : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap map;

    private void Start() => map.RegisterBitVault(gameObject);
}
