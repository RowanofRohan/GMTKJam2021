using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    public GameObject fireParticleGO;
    public GameObject bigExplosionParticle;
    public int radius;
    GameObject explosion;
    List<GameObject> fireParticles;

    private void Awake()
    {
        fireParticles = new List<GameObject>();
    }
    void OnEnable()
    {
        explosion = Instantiate(bigExplosionParticle.gameObject, transform.position, Quaternion.identity);
        Invoke("BurnGround", 1f);
    }

    private void BurnGround()
    {
        Destroy(explosion);
        for (int i = 0; i < 20; i++)
        {
            var pos = Random.insideUnitCircle * radius;
            GameObject fire = Instantiate(fireParticleGO, transform, false);
            fire.transform.localPosition = new Vector2(pos.x, pos.y);
            fireParticles.Add(fire);
        }

        Invoke("PutOffFire", 5f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IBurnable hit = (IBurnable)collider.gameObject.GetComponent(typeof(IBurnable));
        if (hit != null)
        {
            hit.Burn(1000);
        }
    }

    public void PutOffFire()
    {
        foreach (var item in fireParticles)
        {
            item.GetComponent<ParticleSystem>().Stop();
        }

        Destroy(gameObject, 2f);
    }
}
