using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap map;

    private void Awake() => map.RegisterIce(gameObject);

    private void OnDestroy()
    {
        Message.Publish(new IceDestroyed());
    }
}
