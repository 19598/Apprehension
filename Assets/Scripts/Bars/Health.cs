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
    public float getHealth()
    {
        return currentHealth;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("YOU DIED!");
        }
    }

    public void changeHealthByAmount(float amount)
    {
        currentHealth += amount;
        barRight.value = (currentHealth / maxHealth) * 100;
        barLeft.value = (currentHealth / maxHealth) * 100;
        //picture.color = new Color(picture.color.r, picture.color.g, picture.color.b, (maxHealth-currentHealth) / (maxHealth));
    }

    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;
    }
}
