using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float currentHealth = 100;
    private float maxHealth = 100;
    public Slider barRight;
    public Slider barLeft;
    //public Image picture;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Returns the player's health
    /// </summary>
    /// <returns>Player's current health</returns>
    public float getHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Changes the player health by the specified amount
    /// </summary>
    /// <param name="amount">The amount to change the player health by</param>
    public void changeHealthByAmount(float amount)
    {
        currentHealth += amount;
        barRight.value = (currentHealth / maxHealth) * 100;
        barLeft.value = (currentHealth / maxHealth) * 100;
    }

    /// <summary>
    /// Sets the player health
    /// </summary>
    /// <param name="newHealth">The desired health</param>
    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;
    }
}
