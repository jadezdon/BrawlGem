using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (currentHealth <= 0) Destroy(gameObject);
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        healthBar.value = currentHealth;
        healthBar.maxValue = maxHealth;
    }
}
