using UnityEngine;

public class MouseLeftClickProcessor : MonoBehaviour
{
    private Camera _mainCamera;

    private bool _inputBlocked;

    private void OnEnable()
    {
        _inputBlocked = false;
        Message.Subscribe<PieceMoved>(msg => _inputBlocked = true, this);
        Message.Subscribe<PieceMoveFinished>(msg => _inputBlocked = false, this);
    }

    private void OnDisable() => Message.Unsubscribe(this);

    void Awake() => _mainCamera = Camera.main;

    void Update () 
    {
        if (!Input.GetMouseButtonDown(0) || _inputBlocked) return;
        
        var rawMousePos = Input.mousePosition;
        var adjustedMousePos = rawMousePos + new Vector3(0, 0, -_mainCamera.transform.position.z);
        var mousePos = _mainCamera.ScreenToWorldPoint(adjustedMousePos);
        var clickedTile = new TilePoint(mousePos);
        Message.Publish(new TileIndicated(clickedTile));
    }
}
