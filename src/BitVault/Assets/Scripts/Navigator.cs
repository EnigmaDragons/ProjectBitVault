using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Navigator : ScriptableObject
{
    public void NavigateToMainMenu() => NavigateTo("MainMenu");
    public void NavigateToGameScene() => NavigateTo("GameScene");
    public void NavigateToRewards() => NavigateTo("RewardScene");
    public void NavigateToLevelSelect() => NavigateTo("LevelSelectScene");
    public void NavigateToCredits() => NavigateTo("CreditsScene");
    public void NavigateToDialogue() => NavigateTo("DialogueScene");
    
    private void NavigateTo(string name)
    {
        SceneManager.LoadScene(name);
    }
}
