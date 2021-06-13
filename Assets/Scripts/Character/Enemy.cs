using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, ITetherable<int>
{
    public void Tether(int damageTaken)
    {
        //health.TakeDamage(damageTaken);
        Destroy(gameObject);
    }
}
