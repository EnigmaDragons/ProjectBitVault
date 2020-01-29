using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;

    [SerializeField] private GameObject visuals;
    [SerializeField] private RectTransform barFillRectTransform;
    [SerializeField] private TextMeshProUGUI percentLoadedText;
    [SerializeField] private float timeBeforeShowing;
    
    private AsyncOperation _currentLoadingOperation;
    private bool _isLoading;
    private Vector3 _barFillLocalScale;


    private void Awake()
    {
        if (Instance != null)
        {
            
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _barFillLocalScale = barFillRectTransform.localScale;
        Hide();
    }

    private void Update()
    {
        if (!_isLoading) return;
        
        SetProgress(_currentLoadingOperation.progress);

        if (_currentLoadingOperation.isDone) 
            Hide();
    }

    private void SetProgress(float progress)
    {
        _barFillLocalScale.x = progress;
        barFillRectTransform.localScale = _barFillLocalScale;
        percentLoadedText.text = Mathf.CeilToInt(progress * 100) + "%";
    }

    public void Init(AsyncOperation loadingOperation)
    {
        StartCoroutine(BeginShow());
        _currentLoadingOperation = loadingOperation;
        SetProgress(0f);
        _isLoading = true;
    }

    private IEnumerator BeginShow()
    {
        yield return new WaitForSeconds(timeBeforeShowing);
        if (_currentLoadingOperation != null && !_currentLoadingOperation.isDone)
            visuals.SetActive(true);
    }

    public void Hide()
    {
        visuals.SetActive(false);
        _currentLoadingOperation = null;
        _isLoading = false;
    }
}
