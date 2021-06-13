using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int inflictedDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable hit = (IDamageable)collision.gameObject.GetComponent(typeof(IDamageable));
        if (hit != null)
        {
            hit.Damage(inflictedDamage);
        }
    }
}
