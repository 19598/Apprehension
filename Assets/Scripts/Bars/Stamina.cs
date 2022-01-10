using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    private float currentStamina;
    [SerializeField]private float maxStamina = 100;
    public Image picture;
    [SerializeField] PlayerController playerController;
    private void Start()
    {
        currentStamina = maxStamina;
    }

    /// <summary>
    /// Checks the player's stamina and makes them stumble if neccessary
    /// </summary>
    public void Update()
    {
        if (currentStamina < maxStamina && !playerController.stumbling)
        {
            changeStaminaByAmount(20f * Time.deltaTime);
        }
        else if (playerController.stumbling)
        {
            changeStaminaByAmount(2f * Time.deltaTime);
        }
    }

    /// <summary>
    /// Changes stamina by a specified amount
    /// </summary>
    /// <param name="amount">Amount to change stamina by</param>
    public void changeStaminaByAmount(float amount)
    {
        currentStamina += amount;
        picture.color = new Color(picture.color.r, picture.color.g, picture.color.b, (maxStamina-currentStamina)/(maxStamina * 2));
    }

    /// <summary>
    /// Returns the current stamina
    /// </summary>
    /// <returns>Current stamina</returns>
    public float getStamina()
    {
        return currentStamina;
    }

    /// <summary>
    /// Returns the maximum stamina the player can have
    /// </summary>
    /// <returns>Player's max stamina</returns>
    public float getMaxStamina()
    {
        return maxStamina;
    }

    /// <summary>
    /// Sets the stamina of the player
    /// </summary>
    /// <param name="newStamina">Amount to set to</param>
    public void setStamina(float newStamina)
    {
        currentStamina = newStamina;
    }
}
