using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider playerHealthBar;
    [SerializeField] Text playerHealthText;
    [SerializeField] Text gemCountText;


    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.maxValue = player.maxHealth;
        playerHealthBar.value = player.stats.Health;
        playerHealthText.text = "Health: " + player.stats.Health + "/" + player.maxHealth;

        gemCountText.text = player.stats.GemCount.ToString();
    }
}
