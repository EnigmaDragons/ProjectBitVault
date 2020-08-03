using System;
using UnityEngine;

[CreateAssetMenu]
public class LayoutMode : ScriptableObject
{
    public bool IsTall => Screen.height > Screen.width;
    public bool IsWide => Screen.height < Screen.width;
    public bool Is16By9Ratio => !Is4By3Ratio;
    public bool Is4By3Ratio => Math.Abs((float)Screen.width / (float)Screen.height - 4f / 3f) < 0.1 || Math.Abs((float)Screen.height / (float)Screen.width - 4f / 3f) < 0.1;
}
