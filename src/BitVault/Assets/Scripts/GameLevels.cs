using E7.Introloop;
using UnityEngine;

[CreateAssetMenu]
public sealed class GameLevels : ScriptableObject
{
    [SerializeField] private GameLevel[] value;
    [SerializeField] private IntReference starsRequired;
    [SerializeField] private string name;
    [SerializeField] private Sprite logo;
    [SerializeField] private Sprite logoTiled;
    [SerializeField] private Color logoColor;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private IntroloopAudio musicTheme;

    public GameLevel[] Value => value;
    public int StarsRequired => starsRequired;
    public string Name => name;
    public Sprite Logo => logo;
    public Sprite LogoTiled => logoTiled;
    public Color LogoColor => logoColor;
    public Color BackgroundColor => backgroundColor;
    public IntroloopAudio MusicTheme => musicTheme;
}
