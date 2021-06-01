using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] GameObject levelCompletePanel;

    public void ShowLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true);
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        GameController.instance.gameData.MaxUnlockedLevel = Mathf.Max(GameController.instance.gameData.MaxUnlockedLevel, currentLevel + 1);

        // unlock level 2
        if (currentLevel == 1 && !GameController.instance.gameData.AchievedList.Contains(1))
            GameController.instance.gameData.AchievedList.Add(1);
    }

    public void LoadHomeScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadScene(6);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
