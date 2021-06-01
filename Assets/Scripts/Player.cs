using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public class PlayerStats
    {
        public int Health = 100;
        public int Life = 3;
        public int GemCount = 0;
    }

    public PlayerStats stats = new PlayerStats();
    public int maxHealth = 100;

    public void DamagePlayer(int damageValue)
    {
        stats.Health -= damageValue;
        if (stats.Health <= 0)
        {
            LevelManager.Instance.KillPlayer(this);
            stats.Health = maxHealth;
            stats.Life--;
            stats.GemCount = 0;

            // TODO when life equal to 0
        }
    }
}
