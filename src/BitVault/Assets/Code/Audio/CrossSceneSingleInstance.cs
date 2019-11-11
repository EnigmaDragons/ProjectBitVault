using UnityEngine;

public abstract class CrossSceneSingleInstance : MonoBehaviour
{
    void Awake()
    {
        var objs = GameObject.FindGameObjectsWithTag(UniqueTag);
        if (objs.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        OnAwake();
        Debug.Log("Awoken", gameObject);
        DontDestroyOnLoad(gameObject);
    }

    protected abstract string UniqueTag { get; }
    protected abstract void OnAwake();
}