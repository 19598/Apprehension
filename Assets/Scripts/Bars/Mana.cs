using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is not used currently but might be in the future
public class Mana : MonoBehaviour
{
    private float mana = 0;
    public Slider bar;

    /// <summary>
    /// Gets the player's mana
    /// </summary>
    /// <returns>Player's current mana</returns>
    public float getMana()
    {
        return mana;
    }

    /// <summary>
    /// Changes the player's mana by a specified amount
    /// </summary>
    /// <param name="amount">Amount to change by</param>
    public void changeManaByAmount(float amount)
    {
        mana += amount;
        bar.value = mana;
    }

    /// <summary>
    /// Sets the player's mana to specified amount
    /// </summary>
    /// <param name="newMana">Target mana</param>
    public void setMana(float newMana)
    {
        mana = newMana;
    }
}
