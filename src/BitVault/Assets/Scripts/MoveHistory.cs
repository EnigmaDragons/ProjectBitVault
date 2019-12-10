using System;
using UnityEngine;

[CreateAssetMenu]
public sealed class MoveHistory : ScriptableObject
{
    [SerializeField] private GameEvent onChanged;

    private readonly FixedSizeStack<PieceMoved> _moves = new FixedSizeStack<PieceMoved>(3);

    public GameEvent OnChanged => onChanged;
    public void Reset() => Notify(() => _moves.Clear());
    public void Add(PieceMoved p) => Notify(() => _moves.Push(p));
    
    public void Undo()
    {
        if (_moves.Count() > 0)
            Notify(() => _moves.Pop().Undo());
    }

    public int Count => _moves.Count();

    private void Notify(Action a)
    {
        a();
        onChanged.Publish();
    }
}
