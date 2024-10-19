using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestart : MonoBehaviour
{
    // This method will restart the current scene
    public void RestartScene()
    {
        // Get the active scene's name
        string currentScene = SceneManager.GetActiveScene().name;

        // Reload the active scene
        SceneManager.LoadScene(currentScene);
    }
}