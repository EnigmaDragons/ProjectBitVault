using UnityEngine;

public abstract class MovementRule : ScriptableObject
{
    public abstract bool IsValid(GameObject obj, MoveByRequested m);
}
