using UnityEngine;

public class CollisionSnapper : MonoBehaviour
{
    [SerializeField] private bool hasTrigger;
    [SerializeField] private bool isPermaTrigger;
    [SerializeField] private string[] snappableTags;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Animator animator;

    private Rigidbody2D attachedRb = null;

    private readonly int hashTriggeredPara = Animator.StringToHash("Triggered");

    public delegate void CollisionAction();
    public static event CollisionAction OnCollision;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attachedRb == null && CanSnap(other.gameObject.tag))
        {
            attachedRb = other.attachedRigidbody;
            attachedRb.velocity = Vector2.zero;
            attachedRb.angularVelocity = 0;
            attachedRb.transform.position = transform.position;

            OnCollision?.Invoke();

            if (hasTrigger)
            {
                animator.SetBool(hashTriggeredPara, true);
                boxCollider.enabled = false;

                // If one time triggers
                if (isPermaTrigger)
                {
                    // Get All attached colliders
                    Collider2D[] colliders = new Collider2D[8];
                    other.attachedRigidbody.GetAttachedColliders(colliders);

                    // Disable attached colliders
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i] != null)
                        {
                            colliders[i].enabled = false;
                        }
                    }
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody == attachedRb && !isPermaTrigger)
        {
            attachedRb = null;

            if (hasTrigger)
            {
                animator.SetBool(hashTriggeredPara, false);
                boxCollider.enabled = true;
            }

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
