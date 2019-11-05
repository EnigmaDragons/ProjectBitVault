using UnityEngine;

public sealed class MouseRightClickProcessor : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        
        Message.Publish(new PieceDeselected());
    }
}
