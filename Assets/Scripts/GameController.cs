using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Singleton:GameController
    public static GameController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = GameObject.FindGameObjectWithTag("GM").GetComponent<GameController>();
            gameData = SaveManager.Instance.Load();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public GameData gameData;
    public CharacterData currentCharacter;

    void OnApplicationQuit()
    {
        SaveManager.Instance.Save(gameData);
    }
}
