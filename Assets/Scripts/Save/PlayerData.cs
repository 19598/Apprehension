using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position = { 0, 0, 0 };
    public float[] orientation = { 0, 0, 0, 0 };
    public float health;
    public float stamina;
    public float mana;

    /// <summary>
    /// Constructs a data object for saving
    /// </summary>
    /// <param name="player">Player object to be saved</param>
    /// <param name="health">Health object to be saved</param>
    /// <param name="stamina">Stamina object to be saved</param>
    /// <param name="mana">Mana object to be saved</param>
    public PlayerData(PlayerController player, Health health, Stamina stamina, Mana mana)
    {
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        orientation[0] = player.transform.rotation.x;
        orientation[1] = player.transform.rotation.y;
        orientation[2] = player.transform.rotation.z;
        orientation[3] = player.transform.rotation.w;

        this.health = health.getHealth();
        this.stamina = stamina.getStamina();
        this.mana = mana.getMana();

    }

    /// <summary>
    /// Constructs a data object for saving
    /// </summary>
    /// <param name="position">Position to be saved</param>
    /// <param name="rotation">Rotation to be saved</param>
    /// <param name="health">Health value to be saved</param>
    /// <param name="stamina">Stamina value to be saved</param>
    /// <param name="mana">Mana value to be saved</param>
    public PlayerData(float[] position, float[] rotation, float health, float stamina, float mana)
    {
        this.position = position;

        this.orientation = rotation;

        this.health = health;
        this.stamina = stamina;
        this.mana = mana;

    }
}
