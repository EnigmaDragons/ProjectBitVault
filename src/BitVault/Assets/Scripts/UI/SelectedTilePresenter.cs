using UnityEngine;

public sealed class SelectedTilePresenter : MonoBehaviour
{
    [SerializeField] private GameObject selectionIndicator;

    private void OnEnable()
    {
        Message.Subscribe<PieceSelected>(p => Show(p.Piece), this);
        Message.Subscribe<PieceDeselected>(_ => Hide(), this);
    }

    private void OnDisable()
    {
        Message.Unsubscribe(this);
    }

    private void Hide()
    {
        selectionIndicator.transform.SetParent(transform);
        selectionIndicator.SetActive(false);
    }

    private void Show(GameObject p)
    {
        selectionIndicator.transform.position = p.transform.position + new Vector3(0, 0, selectionIndicator.transform.position.z);
        selectionIndicator.transform.SetParent(p.transform);
        selectionIndicator.SetActive(true);
    }
}
