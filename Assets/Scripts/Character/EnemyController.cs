using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public Animator animator;
    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            animator.SetBool("isFollowing", true);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance &&
                 Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            animator.SetBool("isPatrolling", true);
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            animator.SetBool("isFollowing", true);
        }
    }
}
