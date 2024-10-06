using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
   {
    SceneManager.LoadScene(1);
   }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            // Ends the game in editor mode
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Ends the game when built
            Application.Quit();
        #endif
    }
}