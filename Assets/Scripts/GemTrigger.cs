using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTrigger : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // collect the first gem
            if (player.stats.GemCount == 0 && !GameController.instance.gameData.AchievedList.Contains(0))
                GameController.instance.gameData.AchievedList.Add(0);

            player.stats.GemCount++;
            Destroy(this.gameObject);
        }
    }
}
