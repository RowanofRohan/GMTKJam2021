using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherAttack : MonoBehaviour
{
    public int inflictedDamage;
    public Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rb.velocity.magnitude > 1f)
        {
            ITetherable<int> hit = (ITetherable<int>)collision.gameObject.GetComponent(typeof(ITetherable<int>));
            if (hit != null)
            {
                hit.Tether(inflictedDamage);
            }
        }
    }
}
