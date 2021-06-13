using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, ITetherable<int>, IBurnable
{
    public void Burn(int damageTaken)
    {
        health.TakeDamage(damageTaken);
    }

    public void Tether(int damageTaken)
    {
        health.TakeDamage(damageTaken);
        //Destroy(gameObject);
    }
}
