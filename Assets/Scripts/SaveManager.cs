using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public class SaveManager
{
    private static SaveManager instance;

    public static SaveManager Instance
    {
        get
        {
            if (instance == null) instance = new SaveManager();
            return instance;
        }
    }

    public void Save(GameData gameData)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        string path = Application.persistentDataPath + "/game.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, gameData);
        stream.Close();
    }

    public GameData Load()
    {
        GameData gameData;

        string path = Application.persistentDataPath + "/game.save";
        Debug.Log("file path: " + Application.persistentDataPath);
        if (System.IO.File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            FileStream stream = new FileStream(path, FileMode.Open);
            gameData = serializer.Deserialize(stream) as GameData;
            stream.Close();
        }
        else
        {
            gameData = new GameData();
            gameData.MaxUnlockedLevel = 1;
            gameData.TotalGemCount = 20;
            gameData.PurchasedCharacterNameList = new List<string>() { "Archer" };
            gameData.AchievedList = new List<int>();
            gameData.CollectedList = new List<int>();
        }

        return gameData;
    }
}

[System.Serializable]
public class GameData
{
    public int MaxUnlockedLevel;
    public int TotalGemCount;
    public List<string> PurchasedCharacterNameList;
    public List<int> AchievedList;
    public List<int> CollectedList;
}

[System.Serializable]
public class CharacterData
{
    public string Name;
    public Sprite Image;
    public GameObject Prefab;
    public int Price;
}
