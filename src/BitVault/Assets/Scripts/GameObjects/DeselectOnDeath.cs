using UnityEngine;

public class DeselectOnDeath : MonoBehaviour
{
    [SerializeField] private CurrentSelectedPiece piece;

    private void OnDestroy()
    {
        piece.Selected.IfPresent(p =>
        {
            if (gameObject == p)
                Message.Publish(new PieceDeselected());
        });
    }
}
