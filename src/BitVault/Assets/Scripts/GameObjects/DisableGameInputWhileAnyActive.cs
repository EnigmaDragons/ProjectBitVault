using System.Linq;
using UnityEngine;

public class DisableGameInputWhileAnyActive : MonoBehaviour
{
    [SerializeField] private BoolVariable gameInputActive;
    [SerializeField] private GameObject[] gameObjs;

    private void Update() => gameInputActive.Value = !gameObjs.Any(x => x.activeInHierarchy);
}
