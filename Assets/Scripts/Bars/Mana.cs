using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    private float mana = 0;
    public Slider bar;

    /*public void Update()
    {
    }*/
    public float getMana()
    {
        return mana;
    }

    public void changeManaByAmount(float amount)
    {
        mana += amount;
        bar.value = mana;
    }

    public void setMana(float newMana)
    {
        mana = newMana;
    }
}
