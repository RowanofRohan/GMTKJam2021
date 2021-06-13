using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public string tag;
    public Transform objParent;
    public int radius;
    public int objectAmount;

    private void Start()
    {
        Invoke("Spawn", 1f);
    }

    public void Spawn()
    {
        for (int j = 0; j < objectAmount; j++)
        {
            GameObject obj = ObjectPooler.Ins.GetPooledObject(tag);
            var pos = Random.insideUnitCircle * radius;
            obj.SetActive(true);
            obj.transform.SetParent(objParent);
            obj.transform.localPosition = new Vector2(pos.x, pos.y);
        }
    }
}

