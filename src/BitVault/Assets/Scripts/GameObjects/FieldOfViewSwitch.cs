using UnityEngine;

public class FieldOfViewSwitch : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayoutMode layout;
    [SerializeField] private float tallFieldOfView;
    [SerializeField] private float widFieldOfView;
    [SerializeField] private float tallTabletFieldOfView;
    [SerializeField] private float wideTabletFieldOfView;

    private bool isTall;
    private bool isTablet;

    private void Start() => UpdateFieldOfView();

    private void Update()
    {
        if (isTall != layout.IsTall || isTablet != layout.Is4By3Ratio)
            UpdateFieldOfView();
    }

    private void UpdateFieldOfView()
    {
        isTall = layout.IsTall;
        isTablet = layout.Is4By3Ratio;
        camera.fieldOfView = layout.IsTall
            ? layout.Is16By9Ratio
                ? tallFieldOfView
                : tallTabletFieldOfView
            : layout.Is16By9Ratio
                ? widFieldOfView
                : wideTabletFieldOfView;
    }
}
