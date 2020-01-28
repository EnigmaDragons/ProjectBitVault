using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeAcceptanceTest : MonoBehaviour
{
    [SerializeField] private bool runOnAwake;
    
#if UNITY_EDITOR
    void Awake()
    {
        if (runOnAwake)
            RunTest();
    }

    void Start()
    {
        if (!runOnAwake)
            RunTest();
    }

    private void RunTest()
    {
        Message.Publish(new QaTestStarted());
        var issues = GetAllIssues();
        Message.Publish(new QaTestCompleted(issues));
    }
    
    protected abstract List<string> GetAllIssues();
#endif
}
