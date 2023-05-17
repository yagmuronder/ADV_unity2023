using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public int totalCoins = 20;
    public bool allCoinsCollected = false; //initialize to prevent it from being null
    public Animator animator;

    // Other game logic and functionality

    public void CheckAllCoinsCollected()
    {
        if (ScoringSystem.theScore >= totalCoins)
        {
            allCoinsCollected = true;
        }
    }

    public void RestartGame()
    {
        // Add code here to reset game state, reload scene, or perform other actions to restart the game
        // For example, you can use SceneManager.LoadScene(sceneName) to reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
