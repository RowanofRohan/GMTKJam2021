using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IDamageable
{
    public void Damage(int damageTaken)
    {
        health.TakeDamage(damageTaken);
    }
}
