using UnityEngine;

public class CurrentSelectedPiece : ScriptableObject
{
    [SerializeField] private Maybe<GameObject> selected = new Maybe<GameObject>();

    public Maybe<GameObject> Selected => selected;

    public void Select(GameObject obj) => selected = obj;
    public void Deselect() => selected = new Maybe<GameObject>();
}
