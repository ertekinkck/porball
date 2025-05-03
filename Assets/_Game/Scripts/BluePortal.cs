using UnityEngine;

public class BluePortal : MonoBehaviour
{
    public float slowMultiplier = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity *= slowMultiplier;
            }
        }
    }
}
