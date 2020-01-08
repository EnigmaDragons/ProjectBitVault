using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int minSpawns;
    [SerializeField] private int maxSpawns;
    [SerializeField] private Vector3 minBounds;
    [SerializeField] private Vector3 maxBounds;

    public void Start()
    {
        for (var i = 0; i < Rng.Int(minSpawns, maxSpawns + 1); i++)
        {
            var spawn = Instantiate(prefab, transform);
            spawn.transform.localPosition = new Vector3(Rng.Int((int)minBounds.x, (int)maxBounds.x), Rng.Int((int)minBounds.y, (int)maxBounds.y), Rng.Int((int)minBounds.z, (int)maxBounds.z));
        }
    }
}
