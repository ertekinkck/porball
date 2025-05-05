using UnityEngine;

public class GreenPortal : MonoBehaviour
{
    public float speedMultiplier = 1.5f;
    public AudioClip ballHitClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity *= speedMultiplier;
            }
            ballHitClip.PlayClip2D(this, 0.5f);
            ballController.ChangeTrailColor(Color.green);
            MusicManager.Singelton.pitch += 0.2f;
        }
    }
}
