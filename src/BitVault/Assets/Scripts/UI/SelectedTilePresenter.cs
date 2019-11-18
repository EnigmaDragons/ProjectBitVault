using UnityEngine;

public sealed class SelectedTilePresenter : MonoBehaviour
{
    [SerializeField] private GameObject indicatorPrototype;
    private GameObject _selectionIndicator;

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
        if (!_selectionIndicator)
            _selectionIndicator = Instantiate(indicatorPrototype, transform);
        _selectionIndicator.transform.SetParent(transform);
        _selectionIndicator.SetActive(false);
    }
    
    private void Show(GameObject p)
    {
        if (!_selectionIndicator)
            _selectionIndicator = Instantiate(indicatorPrototype, transform);

        var parentPos = p.transform.position;
        _selectionIndicator.transform.position = new Vector3(parentPos.x, parentPos.y, _selectionIndicator.transform.position.z);
        _selectionIndicator.transform.SetParent(p.transform);
        _selectionIndicator.SetActive(true);
    }
}
