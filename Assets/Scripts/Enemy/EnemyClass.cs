using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyClass : MonoBehaviour
{
    //float maxHealth;
    //float health;
    //float type;

    public abstract float getHealth();

    public abstract void setHealth(float newHealth);

    public abstract void changeHealthByAmount(float amount);

    public abstract float returnType();
}
