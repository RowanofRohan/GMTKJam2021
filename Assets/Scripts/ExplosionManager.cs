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
    public Vector2 motionThreshold;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.magnitude > motionThreshold.magnitude)
        {
            //Tetherable
            Explode(transform.position);
            gameObject.SetActive(false);
        }
    }

    public void Explode(Vector2 position)
    {
        GameObject fire = Instantiate(fireAreaGO, position, Quaternion.identity);
    }
}
