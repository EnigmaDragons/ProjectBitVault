using UnityEngine;

[CreateAssetMenu]
public sealed class MoveHistory : ScriptableObject
{
    private readonly FixedSizeStack<PieceMoved> _moves = new FixedSizeStack<PieceMoved>(3);

    public void Reset() => _moves.Clear();
    public void Add(PieceMoved p) => _moves.Push(p);
    
    public void Undo()
    {
        if (_moves.Count() > 0)
            _moves.Pop().Undo();
    }
}
