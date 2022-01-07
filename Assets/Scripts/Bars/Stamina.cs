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

    public void changeStaminaByAmount(float amount)
    {
        currentStamina += amount;
        picture.color = new Color(picture.color.r, picture.color.g, picture.color.b, (maxStamina-currentStamina)/(maxStamina * 2));
    }
    public float getStamina()
    {
        return currentStamina;
    }

    public float getMaxStamina()
    {
        return maxStamina;
    }

    public void setStamina(float newStamina)
    {
        currentStamina = newStamina;
    }
}
