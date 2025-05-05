using UnityEngine;
using DG.Tweening;

public class GoldPortal : MonoBehaviour
{
    public AudioClip ballHitClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.transform.DOMove(transform.position, 0.25f);
            GameManager.Singleton.NextLevel();
            ballHitClip.PlayClip2D(this, 0.5f);
        }
    }
}
