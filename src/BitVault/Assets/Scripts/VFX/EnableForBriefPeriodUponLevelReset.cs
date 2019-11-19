using System.Collections;
using UnityEngine;

public class EnableForBriefPeriodUponLevelReset : OnMessage<LevelReset>
{
    [SerializeField] private FloatReference duration = new FloatReference(1f);
    [SerializeField] private GameObject target;

    private bool _isSetup;

    protected override void Execute(LevelReset msg)
    {
        if (_isSetup)
            StartCoroutine(Activate());
        _isSetup = true;
    }

    private IEnumerator Activate()
    {
        target.SetActive(true);
        yield return new WaitForSeconds(duration);
        target.SetActive(false);
    }
}
