using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public GameObject fireAreaGO;
    public int radius;
    public int inflictedDamage;
    public LayerMask layerMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Tetherable
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
            Explode(transform.position);
    }

    public void Explode(Vector2 position)
    {
        GameObject fire = Instantiate(fireAreaGO, position, Quaternion.identity);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(fire.transform.position, radius);

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                IBurnable hit = (IBurnable)colliders[i].gameObject.GetComponent(typeof(IBurnable));
                if (hit != null)
                {
                    hit.Burn(inflictedDamage);
                }
            }
        }
    }
}
