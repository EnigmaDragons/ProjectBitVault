using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Navigator : ScriptableObject
{
    public void NavigateToMainMenu() => NavigateTo("MainMenu");
    public void NavigateToGameScene() => NavigateTo("GameScene");
    public void NavigateToLevelSelect() => NavigateTo("LevelSelectScene");
    public void NavigateToCredits() => NavigateTo("CreditsScene");
    
    private void NavigateTo(string name)
    {
        var progress = SceneManager.LoadSceneAsync(name);
    }
}
