using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherAttack : MonoBehaviour
{
    public int inflictedDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ITetherable<int> hit = (ITetherable<int>)collision.gameObject.GetComponent(typeof(ITetherable<int>));
        if (hit != null)
        {
            hit.Tether(inflictedDamage);
        }
    }
}
