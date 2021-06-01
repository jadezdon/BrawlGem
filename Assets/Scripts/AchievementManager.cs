using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] Transform scrollView;
    [SerializeField] public List<AchievementItem> achievementItems;
    GameObject item;
    GameObject gameObj;
    Button collectButton;

    void Awake()
    {
        item = scrollView.GetChild(0).gameObject;

        for (int i = 0; i < achievementItems.Count; i++)
        {
            gameObj = Instantiate(item, scrollView);
            if (GameController.instance.gameData.AchievedList.Contains(achievementItems[i].Id))
                achievementItems[i].IsAchieved = true;
            if (GameController.instance.gameData.CollectedList.Contains(achievementItems[i].Id))
                achievementItems[i].IsCollected = true;

            // Text
            gameObj.transform.GetChild(0).GetComponent<Text>().text = achievementItems[i].Description;
            // CollectButton
            collectButton = gameObj.transform.GetChild(1).GetComponent<Button>();
            if (achievementItems[i].IsCollected)
            {
                collectButton.interactable = false;
                collectButton.transform.GetChild(0).GetComponent<Text>().text = "Collected";
            }
            else
            {
                collectButton.gameObject.SetActive(achievementItems[i].IsAchieved);
            }
            collectButton.AddEventListener(i, OnCollectButtonClicked);
            // LockedImage
            gameObj.transform.GetChild(2).GetComponent<Image>().gameObject.SetActive(!achievementItems[i].IsAchieved);
            // GemAmount
            gameObj.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "+" + achievementItems[i].GemAmount;
        }

        Destroy(item);

        // GemHolder
        gameObject.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GameController.instance.gameData.TotalGemCount.ToString();
    }

    void OnCollectButtonClicked(int itemIndex)
    {
        achievementItems[itemIndex].IsCollected = true;
        collectButton = scrollView.GetChild(itemIndex).GetChild(1).GetComponent<Button>();
        collectButton.interactable = false;
        collectButton.transform.GetChild(0).GetComponent<Text>().text = "Collected";

        // update gamedata
        GameController.instance.gameData.CollectedList.Add(achievementItems[itemIndex].Id);
        GameController.instance.gameData.TotalGemCount += achievementItems[itemIndex].GemAmount;
        gameObject.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GameController.instance.gameData.TotalGemCount.ToString();
    }
}

[System.Serializable]
public class AchievementItem
{
    public int Id;
    public string Description;
    public int GemAmount;
    public bool IsAchieved;
    public bool IsCollected;
}
