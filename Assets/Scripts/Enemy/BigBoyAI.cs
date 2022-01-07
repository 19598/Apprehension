using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoyAI : EnemyClass
{
    private float health;
    private float type = 1;

    public override float getHealth()
    {
        return health;
    }

    public override void setHealth(float newHealth)
    {
        health = newHealth;
    }

    public override void changeHealthByAmount(float amount)
    {
        health += amount;
    }

    public override float returnType()
    {
        return type;
    }
}
