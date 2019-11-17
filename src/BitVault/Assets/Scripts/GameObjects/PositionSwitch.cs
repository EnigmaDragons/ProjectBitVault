using UnityEngine;

public class PositionSwitch : MonoBehaviour
{
    [SerializeField] private LayoutMode layout;
    [SerializeField] private Vector3 tallPosition;
    [SerializeField] private Vector3 tallRotation; 
    [SerializeField] private Vector3 widePosition;
    [SerializeField] private Vector3 wideRotation;
    [SerializeField] private bool effectsRotation;
    [SerializeField] private bool effectsX;
    [SerializeField] private bool effectsY;
    [SerializeField] private bool effectsZ;

    private void Update()
    {
        if (effectsRotation)
            transform.localEulerAngles = layout.IsTall ? tallRotation : wideRotation;
        transform.localPosition = new Vector3(effectsX ? (layout.IsTall ? tallPosition.x : widePosition.x) : transform.localPosition.x,
            effectsY ? (layout.IsTall ? tallPosition.y : widePosition.y) : transform.localPosition.y,
            effectsZ ? (layout.IsTall ? tallPosition.z : widePosition.z) : transform.localPosition.z);
    }
}
