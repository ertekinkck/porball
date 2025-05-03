using UnityEngine;

public class BlackPortal : MonoBehaviour
{
    public ParticleSystem ballExpolisionParticleSystem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0;
                other.gameObject.SetActive(false);
                Instantiate(ballExpolisionParticleSystem, other.transform.position, Quaternion.identity);
                if (!GameManager.Singleton.isLevelComplete &&
                FindObjectsByType<BallController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length <= 0)
                    GameManager.Singleton.RestartLevel();
            }
        }
    }
}
