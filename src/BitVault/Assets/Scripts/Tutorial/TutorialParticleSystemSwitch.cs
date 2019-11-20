using UnityEngine;

public class TutorialParticleSystemSwitch : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float startSizeInTutorial;
    [SerializeField] private BoolVariable tutorialActive;

    private ParticleSystem.MainModule psMain;

    private void Start()
    {
        psMain = particleSystem.main;
        if (tutorialActive.Value)
        {
            psMain.startSize = startSizeInTutorial;
            particleSystem.GetComponent<Renderer>().sortingOrder = 1;
            particleSystem.Clear();
            particleSystem.Emit(1);
        }
    }
}
