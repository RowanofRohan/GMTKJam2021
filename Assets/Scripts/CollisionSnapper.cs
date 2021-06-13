using UnityEngine;

public class CollisionSnapper : MonoBehaviour
{
    [SerializeField] private string[] snappableTags;

    private BoxCollider2D boxCollider;
    private Rigidbody2D attachedRb = null;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attachedRb == null && CanSnap(other.gameObject.tag))
        {
            attachedRb = other.attachedRigidbody;
            attachedRb.velocity = Vector2.zero;
            attachedRb.angularVelocity = 0;
            attachedRb.transform.position = transform.position;
            //boxCollider.enabled = false;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody == attachedRb)
        {
            attachedRb = null;
            //boxCollider.enabled = true;
        }
    }

    private bool CanSnap(string tag)
    {
        for (int i = 0; i < snappableTags.Length; i++)
        {
            if (tag.Equals(snappableTags[i]))
            {
                return true;
            }
        }
        return false;
    }

}
