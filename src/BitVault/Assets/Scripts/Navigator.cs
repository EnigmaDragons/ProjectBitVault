using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Navigator : ScriptableObject
{
    public void NavigateToMainMenu() => NavigateTo("MainMenu");
    
    private void NavigateTo(string name)
    {
        Debug.Log($"Navigating to {name}");
        SceneManager.LoadScene(name);
    }
}
