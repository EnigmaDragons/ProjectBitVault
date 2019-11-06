using UnityEngine;

public sealed class SelectedTilePresenter : MonoBehaviour
{
    [SerializeField] private GameObject indicatorPrototype;

    private GameObject _selectionIndicator;
    
    private void Awake()
    {
        _selectionIndicator = Instantiate(indicatorPrototype, transform);
        _selectionIndicator.SetActive(false);
    }
    
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
        _selectionIndicator.transform.SetParent(transform);
        _selectionIndicator.SetActive(false);
    }

    private void Show(GameObject p)
    {
        _selectionIndicator.transform.position = p.transform.position + new Vector3(0, 0, _selectionIndicator.transform.position.z);
        _selectionIndicator.transform.SetParent(p.transform);
        _selectionIndicator.SetActive(true);
    }
}
