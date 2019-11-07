using UnityEngine;

public abstract class MovementRule : ScriptableObject
{
    public abstract bool IsValid(GameObject piece, MoveToRequested m);
}
