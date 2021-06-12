using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private string[] bounceableTags;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CanBounce(collision.gameObject.tag))
        {
            rb.velocity = Vector2.zero;
        }
    }

    private bool CanBounce(string tag)
    {
        for (int i = 0; i < bounceableTags.Length; i++)
        {
            if (tag.Equals(bounceableTags[i]))
            {
                return true;
            }
        }
        return false;
    }

}
