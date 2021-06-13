using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    public GameObject fireAreaGO;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var pos = Random.insideUnitCircle * 3;
            GameObject fire = Instantiate(fireAreaGO, transform, false);
            fire.transform.localPosition = new Vector2(pos.x, pos.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
