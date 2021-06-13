using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    public GameObject fireParticleGO;
    public int radius;

    void OnEnable()
    {
        for (int i = 0; i < 10; i++)
        {
            var pos = Random.insideUnitCircle * radius;
            GameObject fire = Instantiate(fireParticleGO, transform, false);
            fire.transform.localPosition = new Vector2(pos.x, pos.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IBurnable hit = (IBurnable)collider.gameObject.GetComponent(typeof(IBurnable));
        if (hit != null)
        {
            hit.Burn(1000);
        }
    }
}
