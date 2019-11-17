using UnityEngine;

public class FieldOfViewSwitch : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayoutMode layout;
    [SerializeField] private float tallFieldOfView;
    [SerializeField] private float widFieldOfView;

    private void Update() => camera.fieldOfView = layout.IsTall ? tallFieldOfView : widFieldOfView;
}
