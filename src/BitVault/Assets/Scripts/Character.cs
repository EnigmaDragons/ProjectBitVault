using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite bust;
    [SerializeField, DTValidator.Optional] private Sprite alternateArtBust;
    [SerializeField] private BoolReference useAlternateArt = new BoolReference(false);

    public string Name => name;
    public Sprite Bust => alternateArtBust != null && useAlternateArt ? alternateArtBust : bust;
}
