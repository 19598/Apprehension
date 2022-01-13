using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets up a class structure for enemies so they can be saved
/// </summary>
public abstract class EnemyClass : MonoBehaviour
{

    public abstract float getHealth();

    public abstract void setHealth(float newHealth);

    public abstract void changeHealthByAmount(float amount);

    public abstract float returnType();
}
