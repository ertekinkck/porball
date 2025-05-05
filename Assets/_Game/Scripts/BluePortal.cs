using UnityEngine;
using UnityEngine.SceneManagement;

public class BluePortal : MonoBehaviour
{
    public float slowMultiplier = 0.5f;
    public AudioClip ballHitClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity *= slowMultiplier;
            }
            ballHitClip.PlayClip2D(this, 0.5f);
            MusicManager.Singelton.pitch -= 0.2f;
            ballController.ChangeTrailColor(Color.blue);
        }
    }
}