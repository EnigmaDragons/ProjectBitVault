using UnityEngine;

[CreateAssetMenu]
public class CurrentTutorial : ScriptableObject
{
    [SerializeField] private Dialogue tutorial;

    public Dialogue Tutorial => tutorial;

    public void Set(Dialogue tutor) => tutorial = tutor;
}
