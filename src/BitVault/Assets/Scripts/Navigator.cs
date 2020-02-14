using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Navigator : ScriptableObject
{
    private string _currentScene;
    private string _previousScene;

    public void NavigateToMainMenu() => NavigateTo("MainMenu");
    public void NavigateToGameScene() => NavigateTo("GameScene");
    public void NavigateToRewards() => NavigateTo("RewardScene");
    public void NavigateToLevelSelect() => NavigateTo("LevelSelectScene");
    public void NavigateToCredits() => NavigateTo("CreditsScene");
    public void NavigateToDialogue() => NavigateTo("DialogueScene");
    public void NavigateToArchive() => NavigateTo("StoryArchive");
    public void NavigateToSurvey() => NavigateTo("SurveyPlayer");
    public void NavigateBack() => NavigateTo(_previousScene);

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    private void NavigateTo(string name)
    {
        _previousScene = SceneManager.GetActiveScene().name;
        _currentScene = name;
        var loading = SceneManager.LoadSceneAsync(name);
        if (LoadingScreen.Instance != null)
            LoadingScreen.Instance.Init(loading);
    }
}
