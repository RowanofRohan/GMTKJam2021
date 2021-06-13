using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public GameObject fireAreaGO;

    public void Explode()
    {
        GameObject fire = Instantiate(fireAreaGO, transform, false);
    }

}
