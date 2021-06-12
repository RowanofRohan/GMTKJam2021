using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string[] collidableTags;
    [SerializeField] private string wallTag;

    public delegate void CollisionAction(Rigidbody2D rb);
    public static event CollisionAction OnHitObject;
    public delegate void HitAction(Vector3 pos);
    public static event HitAction OnHitWall;

    private void OnEnable()
    {
        ReturnToPoolAfter(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckCollidableTags(other.gameObject.tag))
        {
            OnHitObject(other.attachedRigidbody);
            ReturnToPool();
        }
        else if (other.gameObject.CompareTag(wallTag))
        {
            OnHitWall(transform.position);
            ReturnToPool();
        }
    }


    private bool CheckCollidableTags(string tag)
    {
        for (int i = 0; i < collidableTags.Length; i++)
        {
            if (string.Equals(tag, collidableTags[i]))
            {
                return true;
            }
        }
        return false;
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator ReturnToPoolAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameObject.SetActive(false);
    }
}
