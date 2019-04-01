using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100f;

        currentHealth = maxHealth;

        healthBar.value = CalaculateHealth();
    }

    float CalaculateHealth()
    {
        return currentHealth / maxHealth;
    }

    public void UpdateHealth(float damage)
    {
        currentHealth = damage;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
       healthBar.value = CalaculateHealth();
    }
}
