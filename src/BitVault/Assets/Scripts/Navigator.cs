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
    public void NavigateToTutorial() => NavigateTo("TutorialScene");
    public void NavigateBack() => NavigateTo(_previousScene);
    public void ExitGame() => Application.Quit();
    
    private void NavigateTo(string name)
    {
        _previousScene = SceneManager.GetActiveScene().name;
        _currentScene = name;
        SceneManager.LoadScene(name);
    }
}
