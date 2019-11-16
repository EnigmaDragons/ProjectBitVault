using UnityEngine;

[CreateAssetMenu]
public class LayoutMode : ScriptableObject
{
    public bool IsTall => Screen.height > Screen.width;
    public bool IsWide => Screen.height < Screen.width;
}
