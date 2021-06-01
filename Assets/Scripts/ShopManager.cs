using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Transform shopScrollView;
    [SerializeField] List<ShopItem> shopItems;
    GameObject itemCharacter;
    GameObject gameObj;
    Button buyButton;
    Button selectButton;

    void Awake()
    {
        itemCharacter = shopScrollView.GetChild(0).gameObject;

        for (int i = 0; i < shopItems.Count; i++)
        {
            gameObj = Instantiate(itemCharacter, shopScrollView);
            if (GameController.instance.gameData.PurchasedCharacterNameList.Contains(shopItems[i].character.Name))
                shopItems[i].IsPurchased = true;

            // Thumbnail
            gameObj.transform.GetChild(0).GetComponent<Image>().sprite = shopItems[i].character.Image;

            // GemImage
            gameObj.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = shopItems[i].character.Price.ToString();

            // BuyButton
            buyButton = gameObj.transform.GetChild(2).GetComponent<Button>();
            buyButton.gameObject.SetActive(!shopItems[i].IsPurchased);
            buyButton.AddEventListener(i, OnBuyButtonClicked);

            // NameText
            gameObj.transform.GetChild(3).GetComponent<Text>().text = shopItems[i].character.Name;

            // SelectButton
            selectButton = gameObj.transform.GetChild(4).GetComponent<Button>();
            selectButton.gameObject.SetActive(shopItems[i].IsPurchased);
            selectButton.AddEventListener(i, OnSelectButtonClicked);

            if (GameController.instance.currentCharacter != null && shopItems[i].character.Name == GameController.instance.currentCharacter.Name)
            {
                selectButton.interactable = false;
            }
        }

        Destroy(itemCharacter);

        // GemHolder
        gameObject.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GameController.instance.gameData.TotalGemCount.ToString();
    }

    void OnBuyButtonClicked(int itemIndex)
    {
        if (shopItems[itemIndex].character.Price > GameController.instance.gameData.TotalGemCount)
        {
            // TODO add warning message
            return;
        }

        shopItems[itemIndex].IsPurchased = true;
        buyButton = shopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
        buyButton.gameObject.SetActive(false);
        selectButton = shopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Button>();
        selectButton.gameObject.SetActive(true);
        GameController.instance.gameData.PurchasedCharacterNameList.Add(shopItems[itemIndex].character.Name);

        // update gem amount
        GameController.instance.gameData.TotalGemCount -= shopItems[itemIndex].character.Price;
        gameObject.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GameController.instance.gameData.TotalGemCount.ToString();
    }

    void OnSelectButtonClicked(int itemIndex)
    {
        // unselect previous item
        for (int i = 0; i < shopItems.Count; i++)
            if (shopItems[i].character.Name == GameController.instance.currentCharacter.Name)
                shopScrollView.GetChild(i).GetChild(4).GetComponent<Button>().interactable = true;

        selectButton = shopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Button>();
        selectButton.interactable = false;
        GameController.instance.currentCharacter = shopItems[itemIndex].character;
    }
}

[System.Serializable]
public class ShopItem
{
    public CharacterData character;
    public bool IsPurchased = false;
}
