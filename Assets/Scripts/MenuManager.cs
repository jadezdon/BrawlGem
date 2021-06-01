using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Transform mainMenu;
    [SerializeField] Transform levelMenu;

    Text currentCharacterNameText;
    Image currentCharacterImage;
    Text totalGemText;
    Button quitButton;

    void Start()
    {
        // main menu
        currentCharacterNameText = mainMenu.GetChild(2).GetComponent<Text>();
        currentCharacterImage = mainMenu.GetChild(3).GetComponent<Image>();
        totalGemText = mainMenu.GetChild(4).GetChild(0).GetComponent<Text>();
        quitButton = mainMenu.GetChild(5).GetComponent<Button>();
        quitButton.onClick.AddListener(OnQuitButtonClicked);


        // level menu
        for (int i = GameController.instance.gameData.MaxUnlockedLevel + 1; i <= 5; i++)
        {
            Button button = levelMenu.GetChild(i - 1).GetComponent<Button>();
            button.interactable = false;
            button.transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (currentCharacterNameText.text != GameController.instance.currentCharacter.Name)
        {
            currentCharacterNameText.text = GameController.instance.currentCharacter.Name;
            currentCharacterImage.sprite = GameController.instance.currentCharacter.Image;
        }

        if (totalGemText.text != GameController.instance.gameData.TotalGemCount.ToString())
        {
            totalGemText.text = GameController.instance.gameData.TotalGemCount.ToString();
        }
    }

    void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
