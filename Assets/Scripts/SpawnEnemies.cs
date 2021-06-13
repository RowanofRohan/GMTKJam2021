using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform enemyParent;
    public int radius;
    public int EnemyAmount;

    private void Start()
    {
        Invoke("Spawn", 1f);
    }

    public void Spawn()
    {
        for (int i = 0; i < EnemyAmount; i++)
        {
            var pos = Random.insideUnitCircle * radius;
            GameObject enemy = Instantiate(enemies[Random.Range(0,1)], transform, false);
            enemy.transform.localPosition = new Vector2(pos.x, pos.y);
        }
    }
}
