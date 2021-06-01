using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] EnemyHealthBar enemyHealthBar;

    public int maxHealth = 50;
    public int currentHealth = 50;

    private void Start()
    {
        enemyHealthBar.SetHealth(currentHealth, maxHealth);
    }

    public void DamageEnemy(int damageValue)
    {
        currentHealth -= damageValue;
        enemyHealthBar.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
            enemyAnimator.SetTrigger("Death");
    }
}
