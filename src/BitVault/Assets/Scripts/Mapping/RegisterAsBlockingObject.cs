using UnityEngine;

public sealed class RegisterAsBlockingObject : MonoBehaviour
{
   [SerializeField] private CurrentLevelMap map;

   void Start() => map.RegisterBlockingObject(gameObject);
}
